using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Shamyr.Cloud.Gateway.Service.SignalR.Hubs;
using Shamyr.Cloud.Gateway.Signal.Messages;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.Services
{
  public class ClientHubService: IClientHubService
  {
    private readonly IHubContext<ClientHub> fHubContext;
    private readonly ITracker fTracker;

    public ClientHubService(IHubContext<ClientHub> hubContext, ITracker tracker)
    {
      fHubContext = hubContext;
      fTracker = tracker;
    }

    public async Task SendEventAsync(IOperationContext context, IdentityUserEventMessageBase @event, string method, CancellationToken cancellationToken)
    {
      if (context is null)
        throw new ArgumentNullException(nameof(context));
      if (@event is null)
        throw new ArgumentNullException(nameof(@event));

      using (fTracker.TrackRequest(context, $"SignalR - Gateway client event - {@event.Resource} {method}.", out var requstContext))
      {
        try
        {
          @event.ChangeContext(requstContext);
          await fHubContext
            .Clients
            .Group(@event.Resource)
            .SendAsync(method, @event, cancellationToken);

          requstContext.Success();
          fTracker.TrackInformation(requstContext, $"Event {@event.GetType()} successfully sent.");
        }
        catch (Exception ex)
        {
          requstContext.Fail();
          fTracker.TrackException(requstContext, ex, $"Error while sending event '{@event.GetType()}'.");
          throw ex;
        }
      }
    }
  }
}
