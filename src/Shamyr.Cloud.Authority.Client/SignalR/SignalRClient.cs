using System;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Extensions.DependencyInjection;
using Shamyr.Logging;
using Shamyr.Operations;

namespace Shamyr.Cloud.Authority.Client.SignalR
{
  [Singleton]
  internal partial class SignalRClient: ISignalRClient
  {
    private readonly SignalRClientState fState;
    private readonly IAuthoritySignalRClientConfig fConfig;
    private readonly ILogger fLogger;
    private readonly IServiceProvider fServiceProvider;

    private Task? fAuthroizeTask;

    public SignalRClient(IServiceProvider serviceProvider)
    {
      fServiceProvider = serviceProvider;
      fLogger = serviceProvider.GetRequiredService<ILogger>();
      fConfig = serviceProvider.GetRequiredService<IAuthoritySignalRClientConfig>();

      var url = fConfig.AuthorityUrl.AppendPath(Routes._Client);
      fState = new SignalRClientState(url, serviceProvider);

      fState.Connection.Closed += ClosedAsync;
      fState.Connection.Reconnecting += ReconnectingAsync;
      fState.Connection.Reconnected += ReconnectedAsync;

      fState.Connection.On<UserLoggedOutEvent>(nameof(IRemoteClient.UserLoggedOutEventAsync), UserLoggedOutEventAsync);
      fState.Connection.On<UserVerifiedChangedEvent>(nameof(IRemoteClient.UserVerifiedChangedEventAsync), UserVerifiedChangedEventAsync);
      fState.Connection.On<TokenConfigurationChangedEvent>(nameof(IRemoteClient.TokenConfigurationChangedAsync), TokenConfigurationChangedEventAsync);
    }

    public async void Connect(CancellationToken cancellationToken)
    {
      using (fLogger.TrackRequest(fState.Context, $"Connecting to {fState.SignalUrl}.", out var requestContext))
      {
        try
        {
          await fState.InitialConnectAsync(cancellationToken);
          fAuthroizeTask = AuthorizeConnectionAsync(requestContext, fState.CancellationSource.Token);
          requestContext.Success();
          fLogger.LogInformation(requestContext, $"Successfully connected to '{fState.SignalUrl}'.");
        }
        catch (Exception ex)
        {
          fLogger.LogException(requestContext, ex);
          requestContext.Fail();
        }
      }
    }

    public async ValueTask DisposeAsync()
    {
      await fState.DisposeAsync();
    }

    private async Task AuthorizeConnectionAsync(ILoggingContext context, CancellationToken cancellationToken)
    {
      async Task Operation(ILoggingContext context, CancellationToken cancellationToken)
      {
        await LoginAsync(fConfig.ClientId, fConfig.ClientSecret, context, cancellationToken);
        await SubscribeResourcesAsync(fConfig.SubscribedResources, context, cancellationToken);
      }

      var operationConfig = new RetryOperationConfig
      {
        RetryCount = null,
        RetryPolicy = new Operations.RetryPolicy(TimeSpan.FromSeconds(60), 2)
      };

      using (fLogger.TrackRequest(context, "Authorizing SignalR connection.", out var requestContext))
      {
        await new RetryOperation(Operation, operationConfig)
          .Catch<Unit, HttpRequestException>(fLogger)
          .Catch<Unit, WebSocketException>(fLogger)
          .Catch<Unit, HubException>(fLogger)
          .Catch<Unit, Exception>(fLogger, true)
          .OnSuccess(requestContext)
          .OnFail(requestContext)
          .OnFailRethrow()
          .ExecuteAsync(requestContext, cancellationToken);
      }
    }
  }
}
