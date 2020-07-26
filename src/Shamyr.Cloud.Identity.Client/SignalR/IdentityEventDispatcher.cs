using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Gateway.Signal.Client;
using Shamyr.Cloud.Gateway.Signal.Messages;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Identity.Client.Factories;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client.SignalR
{
  internal class IdentityEventDispatcher: IIdentityEventDispatcher
  {
    private readonly IIdentityEventHandlerFactory fIdentityEventHandlerFactory;
    private readonly ITracker fTracker;

    public IdentityEventDispatcher(IIdentityEventHandlerFactory identityEventHandlerFactory, ITracker tracker)
    {
      fIdentityEventHandlerFactory = identityEventHandlerFactory;
      fTracker = tracker;
    }

    public async Task DispatchAsync(IdentityEventBase message, CancellationToken cancellationToken)
    {
      if (message is null)
        throw new ArgumentNullException(nameof(message));

      using (fTracker.TrackRequest(message.GetContext(), $"Proccessing identity event message - {message.GetType()}.", out var context))
      {
        try
        {
          var handlers = fIdentityEventHandlerFactory.Create(message).ToArray();

          foreach (var handler in handlers)
            await handler.HandleAsync(message, cancellationToken);

          if (handlers.Length > 0)
            fTracker.TrackInformation(context, $"Message '{message.GetType()}' handled successfuly.");
          else
            fTracker.TrackWarning(context, $"No handler for message '{message.GetType()}' was found.");

          context.Success();
        }
        catch (Exception ex)
        {
          fTracker.TrackException(context, ex, $"Error while handling event of type '{message.GetType()}'.");
          context.Fail();
        }
      }
    }
  }
}
