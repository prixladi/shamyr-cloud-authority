using System;
using System.Threading;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  public interface IGatewayHubClient: IAsyncDisposable
  {
    void ConnectAsync(CancellationToken cancellationToken);
  }
}