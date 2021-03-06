﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Client.Factories;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Client.Facades
{
  internal class EventReactionFacade: IEventReactionFacade
  {
    private readonly IEventReactionFactory fFactory;
    private readonly ILogger fLogger;

    public EventReactionFacade(IEventReactionFactory factory, ILogger logger)
    {
      fFactory = factory;
      fLogger = logger;
    }

    public async Task ReactAsync(EventBase @event, CancellationToken cancellationToken)
    {
      var context = @event.GetContext();

      var reactions = fFactory.Create(@event).ToArray();
      if (reactions.Length == 0)
      {
        fLogger.LogInformation(context, $"No reaction for event '{@event.GetType()}'");
        return;
      }

      // TODO: Paralelized reacting
      foreach (var reaction in reactions)
        await reaction.ReactAsync(@event, cancellationToken);
    }
  }
}
