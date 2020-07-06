using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Shamyr.Cloud.Gateway.Signal.Messages;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  internal partial class GatewayHubClient
  {
    private async Task LoginAsync(IOperationContext context, string clientId, string clientSecret, CancellationToken cancellationToken)
    {
      using (fTracker.TrackRequest(context, $"Gateway Hub Login", out var requestContext))
      {
        try
        {
          var request = new LoginRequest(clientId, clientSecret, requestContext);
          var method = nameof(IRemoteServer.LoginAsync);
          await fHubConnection.InvokeAsync<LoginResponse>(method, request, cancellationToken);
          requestContext.Success();
        }
        catch
        {
          requestContext.Fail();
          throw;
        }
      }
    }

    private async Task SubscribeResourcesAsync(IOperationContext context, string[] resources, CancellationToken cancellationToken)
    {
      using (fTracker.TrackRequest(context, $"Gateway Hub SubscribeResources", out var requestContext))
      {
        try
        {
          var request = new SubscribeIdentityResourcesRequest(resources, requestContext);
          var method = nameof(IRemoteServer.SubscribeResourceAsync);
          await fHubConnection.InvokeAsync<SubscribeIdentityResourcesResponse>(method, request, cancellationToken);
          requestContext.Success();
        }
        catch
        {
          requestContext.Fail();
          throw;
        }
      }
    }

    private async void UserLoggedOutEventAsync(UserLoggedOutEvent @event)
    {
      await HandleEventAsync(@event);
    }

    private async void UserVerificationStatusChangedEventAsync(UserVerificationStatusChangedEvent @event)
    {
      await HandleEventAsync(@event);
    }

    private async Task HandleEventAsync(IdentityUserEventMessageBase @event)
    {
      using (fTracker.TrackRequest(@event.GetContext(), $"SignalR - Gateway client event - {@event.Resource}.", out var requestContext))
      {
        try
        {
          @event.ChangeContext(requestContext);
          // TODO: Find real cancellation token
          await fIdentityEventDispatcher.DispatchAsync(@event, fDisposeCTS.Token);
          requestContext.Success();
        }
        catch
        {
          requestContext.Fail();
          throw;
        }
      }
    }
  }
}
