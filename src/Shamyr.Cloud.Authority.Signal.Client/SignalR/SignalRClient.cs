﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Extensions.DependencyInjection;
using Shamyr.Logging;

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

    public async void ConnectAsync(CancellationToken cancellationToken)
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
      using (fLogger.TrackRequest(context, "Authorizing connection", out var requestContext))
      {
        try
        {
          await LoginAsync(fConfig.ClientId, fConfig.ClientSecret, context, cancellationToken);
          await SubscribeResourcesAsync(fConfig.SubscribedResources, context, cancellationToken);
          requestContext.Success();
        }
        catch (Exception ex)
        {
          fLogger.LogException(context, ex);
          requestContext.Fail();
          throw;
        }
      }
    }
  }
}
