using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Identity.Client.Repositories;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client.Services
{
  public class IdentityService: IIdentityService
  {
    private readonly IUserCacheServicesRepository fUserCachesRepository;
    private readonly IUserLockRepository fUserLockRepository;
    private readonly IIdentityClient fIdentityClient;

    private readonly IServiceProvider fServiceProvider;

    public IdentityService(IServiceProvider serviceProvider)
    {
      fServiceProvider = serviceProvider;

      fUserCachesRepository = serviceProvider.GetRequiredService<IUserCacheServicesRepository>();
      fUserLockRepository = serviceProvider.GetRequiredService<IUserLockRepository>();
      fIdentityClient = serviceProvider.GetRequiredService<IIdentityClient>();
    }

    public async Task<UserModel?> GetUserModelByIdAsync(IOperationContext context, string userId, CancellationToken cancellationToken)
    {
      var cacheServices = fUserCachesRepository.GetCacheServices(fServiceProvider);
      await using var manager = new CachePipelineManager(cacheServices.ToArray(), cancellationToken);
      using (await fUserLockRepository.GetByKey(userId).LockForReadingAsync(cancellationToken))
      {
        var cachedUser = await manager.TryGetCachedUserAsync(userId);
        if (cachedUser != null)
        {
          manager.SetResult(cachedUser, userId);
          return cachedUser;
        }
      }

      using (await fUserLockRepository.GetByKey(userId).LockForWritingAsync(cancellationToken))
      {
        var cachedUser = await manager.TryGetCachedUserAsync(userId);
        if (cachedUser is null)
        {
          cachedUser = await fIdentityClient.GetUserByIdAsync(userId, context, cancellationToken);
          if (cachedUser is null)
            return null;
        }

        manager.SetResult(cachedUser, userId);
        return cachedUser;
      }
    }
  }
}
