using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Shamyr.Cloud.Authority.Service.SignalR.Hubs;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Service.Services
{
  public class ClientHubService: IClientHubService
  {
    private readonly IHubContext<ClientHub> fHubContext;
    private readonly ILogger fLogger;

    public ClientHubService(IHubContext<ClientHub> hubContext, ILogger logger)
    {
      fHubContext = hubContext;
      fLogger = logger;
    }

    public async Task SendEventAsync(UserEventBase @event, string method, ILoggingContext context, CancellationToken cancellationToken)
    {
      if (context is null)
        throw new ArgumentNullException(nameof(context));
      if (@event is null)
        throw new ArgumentNullException(nameof(@event));

      using (fLogger.TrackRequest(context, $"SignalR - Authority client event - {@event.Resource} {method}.", out var requstContext))
      {
        try
        {
          @event.ChangeContext(requstContext);
          await fHubContext
            .Clients
            .Group(@event.Resource)
            .SendAsync(method, @event, cancellationToken);

          requstContext.Success();
          fLogger.LogInformation(requstContext, $"Event {@event.GetType()} successfully sent.");
        }
        catch (Exception ex)
        {
          requstContext.Fail();
          fLogger.LogException(requstContext, ex, $"Error while sending event '{@event.GetType()}'.");
          throw;
        }
      }
    }
  }
}
