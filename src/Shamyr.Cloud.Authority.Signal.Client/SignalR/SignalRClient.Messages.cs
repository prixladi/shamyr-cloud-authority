using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Authority.Client.Facades;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Client.SignalR
{
  internal partial class SignalRClient
  {
    private async Task LoginAsync(string clientId, string clientSecret, ILoggingContext context, CancellationToken cancellationToken)
    {
      using (fLogger.TrackRequest(context, $"Authority Hub Login", out var requestContext))
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

    private async Task SubscribeResourcesAsync(string[] resources, ILoggingContext context, CancellationToken cancellationToken)
    {
      using (fLogger.TrackRequest(context, $"Authority Hub Subscribe resources", out var requestContext))
      {
        try
        {
          var request = new SubscribeEventsRequest(resources, requestContext);
          var method = nameof(IRemoteServer.SubscribeResourceAsync);
          await fState.Connection.InvokeAsync<SubscribeEventsResponse>(method, request, cancellationToken);
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

    private async void UserVerifiedChangedEventAsync(UserVerifiedChangedEvent @event)
    {
      await HandleEventAsync(@event);
    }

    private async void UserDisabledChangedEventAsync(UserDisabledChangedEvent @event)
    {
      await HandleEventAsync(@event);
    }

    private async void UserAdminChangedEventAsync(UserAdminChangedEvent @event)
    {
      await HandleEventAsync(@event);
    }

    private async void TokenConfigurationChangedEventAsync(TokenConfigurationChangedEvent @event)
    {
      await HandleEventAsync(@event);
    }

    private async Task HandleEventAsync(EventBase @event)
    {
      using (fLogger.TrackRequest(@event.GetContext(), $"SignalR - Authority client event - '{@event.GetType()}'.", out var requestContext))
      {
        try
        {
          @event.ChangeContext(requestContext);
          using var scope = fServiceProvider.CreateScope();
          var reactionFacade = scope.ServiceProvider.GetRequiredService<IEventReactionFacade>();
          await reactionFacade.ReactAsync(@event, fState.CancellationSource.Token);
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
