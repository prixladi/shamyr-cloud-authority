using System;
using System.Collections.Generic;
using System.Linq;
using Shamyr.Cloud.Authority.Client.Reactions;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Extensions.Factories;

namespace Shamyr.Cloud.Authority.Client.Factories
{
  internal class EventReactionFactory: FactoryBase<IEventReaction>, IEventReactionFactory
  {
    public EventReactionFactory(IServiceProvider serviceProvider)
      : base(serviceProvider) { }

    public IEnumerable<IEventReaction> Create(EventBase @event)
    {
      return GetComponents().Where(x => x.CanReact(@event));
    }
  }
}
