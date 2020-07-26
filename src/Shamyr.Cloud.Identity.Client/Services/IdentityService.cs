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
    private readonly IIdentityClient fIdentityClient;

    private readonly IServiceProvider fServiceProvider;

    public IdentityService(IServiceProvider serviceProvider)
    {
      fServiceProvider = serviceProvider;

      fUserCachesRepository = serviceProvider.GetRequiredService<IUserCacheServicesRepository>();
      fIdentityClient = serviceProvider.GetRequiredService<IIdentityClient>();
    }

    public async Task<UserModel?> GetUserModelByIdAsync(IOperationContext context, string userId, CancellationToken cancellationToken)
    {
      var cacheServices = fUserCachesRepository.GetCacheServices(fServiceProvider);
      await using var manager = new CachePipelineManager(cacheServices.ToArray(), cancellationToken);
      var user = await manager.TryGetCachedUserAsync(userId);
      if (user == null)
      {
        user = await fIdentityClient.GetUserByIdAsync(userId, context, cancellationToken);
        if (user is null)
          return null;
      }

      manager.SetResult(user, userId);
      return user;
    }
  }
}
