using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Gateway.Signal.Messages;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.DependencyInjection;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  [Singleton]
  internal partial class SignalRClient: ISignalRClient
  {
    private readonly SignalRClientState fState;
    private readonly ISignalRClientConfig fConfig;
    private readonly ITracker fTracker;
    private readonly IServiceProvider fServiceProvider;

    private Task? fAuthroizeTask;

    public SignalRClient(IServiceProvider serviceProvider)
    {
      fServiceProvider = serviceProvider;
      fTracker = serviceProvider.GetRequiredService<ITracker>();
      fConfig = serviceProvider.GetRequiredService<ISignalRClientConfig>();

      var url = fConfig.GatewayUrl.AppendPath(Routes._Client);
      fState = new SignalRClientState(url, serviceProvider);

      fState.Connection.Closed += ClosedAsync;
      fState.Connection.Reconnecting += ReconnectingAsync;
      fState.Connection.Reconnected += ReconnectedAsync;

      fState.Connection.On<UserLoggedOutEvent>(nameof(IRemoteClient.UserLoggedOutEventAsync), UserLoggedOutEventAsync);
      fState.Connection.On<UserVerificationStatusChangedEvent>(nameof(IRemoteClient.UserVerificationStatusChangedEventAsync), UserVerificationStatusChangedEventAsync);
      fState.Connection.On<TokenConfigurationChangedEvent>(nameof(IRemoteClient.TokenConfigurationChangedAsync), TokenConfigurationChangedEventAsync);
    }

    public async void ConnectAsync(CancellationToken cancellationToken)
    {
      using (fTracker.TrackRequest(fState.Context, $"Connecting to {fState.SignalUrl}.", out var requestContext))
      {
        try
        {
          await fState.InitialConnectAsync(cancellationToken);
          fAuthroizeTask = AuthorizeConnectionAsync(requestContext, fState.CancellationSource.Token);
          requestContext.Success();
          fTracker.TrackInformation(requestContext, $"Successfully connected to '{fState.SignalUrl}'.");
        }
        catch (Exception ex)
        {
          fTracker.TrackException(requestContext, ex);
          requestContext.Fail();
        }
      }
    }

    public async ValueTask DisposeAsync()
    {
      await fState.DisposeAsync();
    }

    private async Task AuthorizeConnectionAsync(IOperationContext context, CancellationToken cancellationToken)
    {
      using (fTracker.TrackRequest(context, "Authorizing connection", out var requestContext))
      {
        try
        {
          await LoginAsync(context, fConfig.ClientId, fConfig.ClientSecret, cancellationToken);
          await SubscribeResourcesAsync(context, fConfig.SubscribedResources, cancellationToken);
          requestContext.Success();
        }
        catch (Exception ex)
        {
          fTracker.TrackException(context, ex);
          requestContext.Fail();
          throw;
        }
      }
    }
  }
}
