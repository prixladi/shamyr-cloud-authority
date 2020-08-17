using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Client.Services
{
  internal interface IHubConnectionService
  {
    HubConnection CreateConnection(Uri signalUrl);
    Task InitialConnectAsync(HubConnection hubConnection, Uri signalUrl, ILoggingContext context, CancellationToken cancellationToken);
  }
}