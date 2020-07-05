using System;
using System.Threading;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  internal interface IGatewayHubClient: IAsyncDisposable
  {
    void ConnectAsync(CancellationToken cancellationToken);
  }
}