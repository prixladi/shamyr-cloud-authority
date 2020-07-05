using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Identity.Client.Models;
using Shamyr.Cloud.Identity.Client.Repositories;
using Shamyr.Security.Tokens.Jwt;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client
{
  public class IdentityClient: IIdentityClient
  {
    private readonly IUserCacheServicesRepository fUserCachesRepository;
    private readonly IUserLockRepository fUserLockRepository;
    private readonly IIdentityRemoteClient fIdentityRemoteClient;

    private readonly IServiceProvider fServiceProvider;

    public IdentityClient(IServiceProvider serviceProvider)
    {
      fServiceProvider = serviceProvider;

      fUserCachesRepository = serviceProvider.GetRequiredService<IUserCacheServicesRepository>();
      fUserLockRepository = serviceProvider.GetRequiredService<IUserLockRepository>();
      fIdentityRemoteClient = serviceProvider.GetRequiredService<IIdentityRemoteClient>();
    }

    public async Task<UserIdentityValidationModel?> GetUserIdentityByJwtAsync(IOperationContext context, string jwt, CancellationToken cancellationToken)
    {
      if (!TryGetPropertiesFromJwt(jwt, out var expirationInSeconds, out var userId) || Expired(expirationInSeconds))
        return null;

      var cacheServices = fUserCachesRepository.GetCacheServices(fServiceProvider);
      await using var manager = new CachePipelineManager(cacheServices.ToArray(), cancellationToken);
      using (await fUserLockRepository.GetByKey(userId).LockForReadingAsync(cancellationToken))
      {
        var cachedUser = await manager.TryGetCachedUserAsync(userId);
        if (cachedUser != null && cachedUser.Jwt != null && cachedUser.Jwt == jwt)
        {
          manager.SetResult(cachedUser, userId, expirationInSeconds);
          return cachedUser.ToIdentityModel();
        }
      }

      using (await fUserLockRepository.GetByKey(userId).LockForWritingAsync(cancellationToken))
      {
        var cachedUser = await manager.TryGetCachedUserAsync(userId);
        if (cachedUser is null || cachedUser.Jwt is null || cachedUser.Jwt != jwt)
        {
          var userIdentity = await fIdentityRemoteClient.GetIdentityByJwtAsync(context, jwt, cancellationToken);
          cachedUser = userIdentity.ToCachedUserModel(jwt);
        }

        manager.SetResult(cachedUser, userId, expirationInSeconds);
        return cachedUser.ToIdentityModel();
      }
    }

    public async Task<UserIdentityProfileModel?> GetUserModelByIdAsync(IOperationContext context, string userId, CancellationToken cancellationToken)
    {
      var cacheServices = fUserCachesRepository.GetCacheServices(fServiceProvider);
      await using var manager = new CachePipelineManager(cacheServices.ToArray(), cancellationToken);
      using (await fUserLockRepository.GetByKey(userId).LockForReadingAsync(cancellationToken))
      {
        var cachedUser = await manager.TryGetCachedUserAsync(userId);
        if (cachedUser != null)
        {
          manager.SetResult(cachedUser, userId);
          return cachedUser.Model;
        }
      }

      using (await fUserLockRepository.GetByKey(userId).LockForWritingAsync(cancellationToken))
      {
        var cachedUser = await manager.TryGetCachedUserAsync(userId);
        if (cachedUser is null)
        {
          var userModel = await fIdentityRemoteClient.GetUserByIdAsync(context, userId, cancellationToken);
          if (userModel is null)
            return null;

          cachedUser = new CachedUserModel { Model = userModel };
        }

        manager.SetResult(cachedUser, userId);
        return cachedUser.Model;
      }
    }

    private static bool TryGetPropertiesFromJwt(string jwt, out long expirationInSeconds, out string name)
    {
      expirationInSeconds = 0;
      name = string.Empty;

      if (!JsonWebToken.TryParse(jwt, out var token))
        return false;

      expirationInSeconds = token.Payload.GetValue<long>("exp");
      name = token.Payload.GetValue<string>(ClaimTypes.Name);

      Debug.Assert(name != null);

      return true;
    }

    private static bool Expired(long expirationInSeconds)
    {
      return DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= expirationInSeconds;
    }
  }
}
