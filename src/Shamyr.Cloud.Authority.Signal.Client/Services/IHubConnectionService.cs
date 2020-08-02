using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Client.Services
{
  internal interface IHubConnectionService
  {
    HubConnection CreateConnection(Uri signalUrl);
    Task InitialConnectAsync(HubConnection hubConnection, Uri signalUrl, IOperationContext context, CancellationToken cancellationToken);
  }
}