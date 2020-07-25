using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Identity.Client.Repositories;
using Shamyr.Cloud.Identity.Client.Services;

namespace Shamyr.Cloud.Identity.Client.Handlers
{
  public abstract class UserIdentityEventHandlerBase<TMessage>: IUserIdentityEventHandler
    where TMessage : IdentityUserEventMessageBase
  {
    private readonly IUserCacheServicesRepository fUserCachesRepository;
    private readonly IUserLockRepository fUserLockRepository;

    protected UserIdentityEventHandlerBase(
      IUserCacheServicesRepository userRetrieverRepository,
      IUserLockRepository userLockRepository)
    {
      fUserCachesRepository = userRetrieverRepository;
      fUserLockRepository = userLockRepository;
    }

    public bool CanHandle(IdentityUserEventMessageBase message)
    {
      if (message is null)
        throw new ArgumentNullException(nameof(message));

      return message is TMessage;
    }

    public async Task HandleAsync(IServiceProvider serviceProvider, IdentityUserEventMessageBase message, CancellationToken cancellationToken)
    {
      using (await fUserLockRepository.GetByKey(message.UserId.ToString()).LockForWritingAsync(cancellationToken))
      {
        var caches = fUserCachesRepository.GetCacheServices(serviceProvider);
        var tasks = new List<Task>();

        foreach (var cache in caches)
          tasks.Add(DoHandleAsync(cache, (TMessage)message, cancellationToken));

        await Task.WhenAll(tasks);
      }
    }

    protected virtual Task DoHandleAsync(IUserCacheService userCache, TMessage message, CancellationToken cancellationToken)
    {
      return userCache.RemoveUserAsync(message.UserId.ToString(), cancellationToken);
    }
  }
}
