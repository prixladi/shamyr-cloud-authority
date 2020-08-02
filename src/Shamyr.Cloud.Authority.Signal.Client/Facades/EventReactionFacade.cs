using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Client.Factories;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Client.Facades
{
  internal class EventReactionFacade: IEventReactionFacade
  {
    private readonly IEventReactionFactory fFactory;
    private readonly ITracker fTracker;

    public EventReactionFacade(IEventReactionFactory factory, ITracker tracker)
    {
      fFactory = factory;
      fTracker = tracker;
    }

    public async Task ReactAsync(EventBase @event, CancellationToken cancellationToken)
    {
      var context = @event.GetContext();

      var reactions = fFactory.Create(@event).ToArray();
      if (reactions.Length == 0)
      {
        fTracker.TrackInformation(context, $"No reaction for event '{@event.GetType()}'");
        return;
      }

      // TODO: paralelize reacting
      foreach (var reaction in reactions)
        await reaction.ReactAsync(@event, cancellationToken);
    }
  }
}
