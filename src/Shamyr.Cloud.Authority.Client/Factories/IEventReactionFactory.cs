using System.Collections.Generic;
using Shamyr.Cloud.Authority.Client.Reactions;
using Shamyr.Cloud.Authority.Signal.Messages;

namespace Shamyr.Cloud.Authority.Client.Factories
{
  public interface IEventReactionFactory
  {
    IEnumerable<IEventReaction> Create(EventBase @event);
  }
}