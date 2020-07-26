using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Gateway.Signal.Messages;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  internal partial class SignalRClient
  {
    private async Task LoginAsync(IOperationContext context, string clientId, string clientSecret, CancellationToken cancellationToken)
    {
      using (fTracker.TrackRequest(context, $"Gateway Hub Login", out var requestContext))
      {
        try
        {
          var request = new LoginRequest(clientId, clientSecret, requestContext);
          var method = nameof(IRemoteServer.LoginAsync);
          await fState.Connection.InvokeAsync<LoginResponse>(method, request, cancellationToken);
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
      using (fTracker.TrackRequest(context, $"Gateway Hub Subscribe resources", out var requestContext))
      {
        try
        {
          var request = new SubscribeIdentityResourcesRequest(resources, requestContext);
          var method = nameof(IRemoteServer.SubscribeResourceAsync);
          await fState.Connection.InvokeAsync<SubscribeIdentityResourcesResponse>(method, request, cancellationToken);
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

    private async void TokenConfigurationChangedEventAsync(TokenConfigurationChangedEvent @event)
    {
      using (fTracker.TrackRequest(@event.GetContext(), $"SignalR - Gateway client event - {@event.GetType()}.", out var requestContext))
      {
        try
        {
          @event.ChangeContext(requestContext);
          using var scope = fServiceProvider.CreateScope();
          var dispatcher = scope.ServiceProvider.GetRequiredService<IIdentityEventDispatcher>();
          await dispatcher.DispatchAsync(@event, fState.CancellationSource.Token);
          requestContext.Success();
        }
        catch
        {
          requestContext.Fail();
          throw;
        }
      }
    }

    private async Task HandleEventAsync(IdentityUserEventBase @event)
    {
      using (fTracker.TrackRequest(@event.GetContext(), $"SignalR - Gateway client user event resource - {@event.Resource}.", out var requestContext))
      {
        try
        {
          @event.ChangeContext(requestContext);
          using var scope = fServiceProvider.CreateScope();
          var dispatcher = scope.ServiceProvider.GetRequiredService<IIdentityEventDispatcher>();
          await dispatcher.DispatchAsync(@event, fState.CancellationSource.Token);
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
