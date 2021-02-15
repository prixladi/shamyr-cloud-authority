using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Shamyr.Cloud.Authority.Client.SignalR
{
  internal class SignalRClientStarterService: IHostedService
  {
    private readonly ISignalRClient fHubClient;

    public SignalRClientStarterService(ISignalRClient hubClient)
    {
      fHubClient = hubClient;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      Task.Run(() => fHubClient.Connect(cancellationToken), cancellationToken);
      return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
      await fHubClient.DisposeAsync().AsTask();
    }
  }
}
