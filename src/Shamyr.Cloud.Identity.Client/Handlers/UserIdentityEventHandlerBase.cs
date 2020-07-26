using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Identity.Client.Repositories;
using Shamyr.Cloud.Identity.Client.Services;

namespace Shamyr.Cloud.Identity.Client.Handlers
{
  internal abstract class UserIdentityEventHandlerBase<TMessage>: IdentityEventHandlerBase<TMessage>
    where TMessage : IdentityUserEventBase
  {
    private readonly IUserCacheServicesRepository fUserCachesRepository;
    private readonly IUserLockRepository fUserLockRepository;
    private readonly IServiceProvider fServiceProvider;

    protected UserIdentityEventHandlerBase(IServiceProvider serviceProvider)
    {
      fUserCachesRepository = serviceProvider.GetRequiredService<IUserCacheServicesRepository>();
      fUserLockRepository = serviceProvider.GetRequiredService<IUserLockRepository>();
      fServiceProvider = serviceProvider;
    }

    protected override async Task DoHandleAsync(TMessage message, CancellationToken cancellationToken)
    {
      using (await fUserLockRepository.GetByKey(message.UserId.ToString()).LockForWritingAsync(cancellationToken))
      {
        var caches = fUserCachesRepository.GetCacheServices(fServiceProvider);
        var tasks = new List<Task>();

        foreach (var cache in caches)
          tasks.Add(DoHandleAsync(cache, message, cancellationToken));

        await Task.WhenAll(tasks);
      }
    }

    protected virtual Task DoHandleAsync(IUserCacheService userCache, TMessage message, CancellationToken cancellationToken)
    {
      return userCache.RemoveUserAsync(message.UserId.ToString(), cancellationToken);
    }
  }
}
