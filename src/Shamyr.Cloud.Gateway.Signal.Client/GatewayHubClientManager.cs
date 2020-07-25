using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  internal class SignalRClientManager: IHostedService
  {
    private readonly IGatewayHubClient fHubClient;

    public SignalRClientManager(IGatewayHubClient hubClient)
    {
      fHubClient = hubClient;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      fHubClient.ConnectAsync(cancellationToken);
      return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
      await fHubClient.DisposeAsync().AsTask();
    }
  }
}
