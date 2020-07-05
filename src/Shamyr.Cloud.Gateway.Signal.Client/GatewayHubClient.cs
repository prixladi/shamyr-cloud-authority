using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Shamyr.Cloud.Gateway.Signal.Client.Services;
using Shamyr.Cloud.Gateway.Signal.Messages;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.DependencyInjection;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  [Singleton]
  internal partial class GatewayHubClient: IGatewayHubClient
  {
    private readonly CancellationTokenSource fDisposeCTS;
    private readonly HubConnection fHubConnection;

    private readonly IOperationContext fHubOperationContext;
    private readonly string fSignalUrl;

    private Task? fAuthroizeTask;

    private readonly IHubClientConfig fHubClientConfig;
    private readonly IIdentityEventDispatcher fIdentityEventDispatcher;
    private readonly IHubService fHubService;
    private readonly ITracker fTracker;

    public GatewayHubClient(
      IHubClientConfig hubClientConfig,
      IIdentityEventDispatcher identityEventDispatcher,
      IHubService hubService,
      ITracker tracker)
    {
      fHubClientConfig = hubClientConfig;
      fIdentityEventDispatcher = identityEventDispatcher;
      fHubService = hubService;
      fTracker = tracker;

      fHubOperationContext = OperationContext.Origin;
      fSignalUrl = Path.Combine(hubClientConfig.GatewayUrl, Routes._Client);
      fDisposeCTS = new CancellationTokenSource();
      fHubConnection = fHubService.CreateConnection(fSignalUrl);

      fHubConnection.Closed += ClosedAsync;
      fHubConnection.Reconnecting += ReconnectingAsync;
      fHubConnection.Reconnected += ReconnectedAsync;

      fHubConnection.On<UserLoggedOutEvent>(nameof(IRemoteClient.UserLoggedOutEventAsync), UserLoggedOutEventAsync);
      fHubConnection.On<UserUserPermissionChangedEvent>(nameof(IRemoteClient.UserUserPermissionChangedEventAsync), UserUserPermissionChangedEventAsync);
      fHubConnection.On<UserVerificationStatusChangedEvent>(nameof(IRemoteClient.UserVerificationStatusChangedEventAsync), UserVerificationStatusChangedEventAsync);
    }

    public async void ConnectAsync(CancellationToken cancellationToken)
    {
      using (fTracker.TrackRequest(fHubOperationContext, $"Connecting to {fSignalUrl}.", out var requestContext))
      {
        try
        {
          await fHubService.InitialConnectAsync(fHubConnection, fSignalUrl, requestContext, cancellationToken);
          fAuthroizeTask = AuthorizeConnectionAsync(requestContext, fDisposeCTS.Token);

          requestContext.Success();
          fTracker.TrackInformation(requestContext, $"Successfully connected to '{fSignalUrl}'.");
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
      fDisposeCTS.Cancel();

      using (fTracker.TrackRequest(fHubOperationContext, $"Disconecting from {fSignalUrl}.", out var requestContext))
      {
        try
        {
          await fHubConnection.DisposeAsync();

          requestContext.Success();
        }
        catch (Exception ex)
        {
          fTracker.TrackException(requestContext, ex);

          requestContext.Fail();
          throw;
        }
      }
    }

    private Task ClosedAsync(Exception ex)
    {
      if (ex is null)
        fTracker.TrackError(fHubOperationContext, $"Connection with url '{fSignalUrl}' closed.");
      else
        fTracker.TrackException(fHubOperationContext, ex, $"Connection with url '{fSignalUrl}' closed with exception.");

      return Task.CompletedTask;
    }

    private Task ReconnectingAsync(Exception ex)
    {
      if (ex is null)
        fTracker.TrackInformation(fHubOperationContext, $"Reconecting to url '{fSignalUrl}'.");
      else
        fTracker.TrackException(fHubOperationContext, ex, $"Reconecting to url '{fSignalUrl}'.");

      return Task.CompletedTask;
    }

    private async Task ReconnectedAsync(string connectionId)
    {
      if (string.IsNullOrEmpty(connectionId))
        fTracker.TrackInformation(fHubOperationContext, $"Reconected to url '{fSignalUrl}' with old connection id.");
      else
        fTracker.TrackInformation(fHubOperationContext, $"Reconected to url '{fSignalUrl}' with connection id '{connectionId}'.");

      if (fAuthroizeTask != null)
        await fAuthroizeTask;
      fAuthroizeTask = AuthorizeConnectionAsync(fHubOperationContext, fDisposeCTS.Token);
    }

    private async Task AuthorizeConnectionAsync(IOperationContext context, CancellationToken cancellationToken)
    {
      using (fTracker.TrackRequest(context, "Authorizing connection", out var requestContext))
      {
        try
        {
          await LoginAsync(context, fHubClientConfig.ClientId, fHubClientConfig.ClientSecret, cancellationToken);
          await SubscribeResourcesAsync(context, fHubClientConfig.SubscribedResources, cancellationToken);
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
