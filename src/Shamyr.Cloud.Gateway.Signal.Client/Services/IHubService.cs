using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Client.Services
{
  internal interface IHubService
  {
    HubConnection CreateConnection(string signalUrl);
    Task InitialConnectAsync(HubConnection hubConnection, string signalUrl, IOperationContext context, CancellationToken cancellationToken);
  }
}