using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Gateway.Signal.Client;
using Shamyr.Cloud.Gateway.Signal.Messages;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Identity.Client.Factories;
using Shamyr.DependencyInjection;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client.SignalR
{
  [Singleton]
  internal class IdentityEventDispatcher: IIdentityEventDispatcher
  {
    private readonly IServiceProvider fServiceProvider;
    private readonly ITracker fTracker;
    private readonly IUserIdentityEventHandlerFactory fHandlerFactory;

    public IdentityEventDispatcher(IServiceProvider serviceProvider)
    {
      fServiceProvider = serviceProvider;
      fTracker = serviceProvider.GetRequiredService<ITracker>();
      fHandlerFactory = serviceProvider.GetRequiredService<IUserIdentityEventHandlerFactory>();
    }

    public async Task DispatchAsync(IdentityUserEventMessageBase message, CancellationToken cancellationToken)
    {
      if (message is null)
        throw new ArgumentNullException(nameof(message));

      using (fTracker.TrackRequest(message.GetContext(), $"Proccessing identity event message - {message.GetType()}.", out var context))
      {
        try
        {
          var handlers = fHandlerFactory.Create(message);

          foreach (var handler in handlers)
            await handler.HandleAsync(fServiceProvider, message, cancellationToken);

          if (handlers.Any())
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
