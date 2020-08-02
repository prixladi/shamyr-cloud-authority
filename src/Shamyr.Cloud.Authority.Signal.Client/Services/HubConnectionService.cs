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
using Shamyr.Operations;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Client.Services
{
  internal class HubConnectionService: IHubConnectionService
  {
    private readonly ITracker fTracker;

    public HubConnectionService(ITracker tracker)
    {
      fTracker = tracker;
    }

    public HubConnection CreateConnection(Uri signalUrl)
    {
      return new HubConnectionBuilder()
        .WithUrl(signalUrl, SetupUrl)
        .AddJsonProtocol(SetupJsonProtocol)
        .WithAutomaticReconnect(new SignalR.RetryPolicy(TimeSpan.FromSeconds(30), 2))
        .Build();
    }

    public async Task InitialConnectAsync(HubConnection hubConnection, Uri signalUrl, IOperationContext context, CancellationToken cancellationToken)
    {
      var config = new RetryOperationConfig
      {
        RetryCount = null,
        RetryPolicy = new RetryPolicy(TimeSpan.FromSeconds(60), 2),
        SameExceptions = SameExceptions
      };

      await new RetryOperation((_, cancellationToken) => hubConnection.StartAsync(cancellationToken), config)
        .Catch<Unit, WebSocketException>(fTracker)
        .OnRetryInformation(fTracker, $"Retrying to connect to '{signalUrl}' ...")
        .OnSuccess(fTracker, $"Successfuly connected to '{signalUrl}'.")
        .OnFail(fTracker, $"Unable to connect to '{signalUrl}'.")
        .OnFailRethrow()
        .ExecuteAsync(context, cancellationToken);
    }

    private bool SameExceptions(Exception ex1, Exception ex2)
    {
      if (ex1 is WebSocketException we1 && ex2 is WebSocketException we2)
        return we1.ErrorCode == we2.ErrorCode;

      return ex1.GetType() == ex2.GetType();
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
