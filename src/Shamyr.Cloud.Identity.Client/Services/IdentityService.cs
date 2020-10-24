using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Identity.Client.Factories;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Logging;

namespace Shamyr.Cloud.Identity.Client.Services
{
  internal class IdentityService: IIdentityService
  {
    private readonly IIdentityClient fIdentityClient;
    private readonly ICachePipelineFactory fCachePipelineFactory;
    private readonly IServiceProvider fServiceProvider;

    public IdentityService(IServiceProvider serviceProvider)
    {
      fServiceProvider = serviceProvider;
      fCachePipelineFactory = serviceProvider.GetRequiredService<ICachePipelineFactory>();
      fIdentityClient = serviceProvider.GetRequiredService<IIdentityClient>();
    }

    public async Task<UserModel?> GetUserModelByIdAsync(string userId, ILoggingContext context, CancellationToken cancellationToken)
    {
      var pipeline = fCachePipelineFactory.Create();

      using var scope = fServiceProvider.CreateScope();
      await using var manager = new CachePipelineManager(pipeline, scope.ServiceProvider, cancellationToken);

      var user = await manager.TryGetCachedUserAsync(userId);
      if (user is null)
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
