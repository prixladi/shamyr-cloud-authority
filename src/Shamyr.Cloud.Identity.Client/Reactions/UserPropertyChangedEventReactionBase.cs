using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Authority.Client.Reactions;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Cloud.Identity.Client.Factories;
using Shamyr.Cloud.Identity.Client.Repositories;
using Shamyr.Cloud.Identity.Client.Services;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Client.Reactions
{
  public abstract class UserPropertyChangedEventReactionBase<TEvent>: IEventReaction
    where TEvent : UserEventBase
  {
    private readonly ICachePipelineFactory fCachePipelineFactory;
    private readonly IServiceProvider fServiceProvider;

    protected UserPropertyChangedEventReactionBase(IServiceProvider serviceProvider)
    {
      fCachePipelineFactory = serviceProvider.GetRequiredService<ICachePipelineFactory>();
      fServiceProvider = serviceProvider;
    }

    public bool CanReact(EventBase @event)
    {
      return @event is TEvent;
    }

    public async Task ReactAsync(EventBase @event, CancellationToken cancellationToken)
    {
      var pipeline = fCachePipelineFactory.Create();

      var tasks = new LinkedList<Task>();

      foreach (var service in pipeline.GetServices(fServiceProvider))
        tasks.AddLast(ReactAsync(service, (TEvent)@event, cancellationToken));

      await Task.WhenAll(tasks);
    }

    // TODO: Don't mutate, use with expression
    protected abstract UserModel MutateUserAsync(UserModel model, TEvent @event);

    private async Task ReactAsync(IUserCacheService cache, TEvent @event, CancellationToken cancellationToken)
    {
      var user = await cache.RetrieveUserAsync(@event.UserId, cancellationToken);
      if (user is null)
        return;

      var newUser = MutateUserAsync(user, @event);
      await cache.SaveUserAsync(@event.UserId, newUser, cancellationToken);
    }
  }
}
