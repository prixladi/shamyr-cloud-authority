using System;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Client.Services
{
  internal class HubService: IHubService
  {
    private readonly ITracker fTracker;

    public HubService(ITracker tracker)
    {
      fTracker = tracker;
    }

    public HubConnection CreateConnection(string signalUrl)
    {
      return new HubConnectionBuilder()
        .WithUrl(signalUrl, SetupUrl)
        .AddJsonProtocol(SetupJsonProtocol)
        .WithAutomaticReconnect(new RetryPolicy(TimeSpan.FromSeconds(30), 2))
        .Build();
    }

    public async Task InitialConnectAsync(HubConnection hubConnection, string signalUrl, IOperationContext context, CancellationToken cancellationToken)
    {
      int? lastErrorCode = null;
      int nextDelay = 1;
      do
      {
        try
        {
          await hubConnection.StartAsync(cancellationToken);
          break;
        }
        catch (WebSocketException ex)
        {
          if (lastErrorCode != ex.ErrorCode)
          {
            fTracker.TrackException(context, ex);
            lastErrorCode = ex.ErrorCode;
          }

          await Task.Delay(TimeSpan.FromSeconds(nextDelay), cancellationToken);
          nextDelay = Math.Min(60, nextDelay * 2);
          fTracker.TrackInformation(context, $"Retrying to connect to '{signalUrl}' ...");
        }
      } while (!cancellationToken.IsCancellationRequested);
    }

    private void SetupUrl(HttpConnectionOptions options)
    {
      options.Transports = HttpTransportType.WebSockets;
      options.SkipNegotiation = true;
    }

    private void SetupJsonProtocol(JsonHubProtocolOptions options)
    {
      options.PayloadSerializerOptions = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      };
    }
  }
}
