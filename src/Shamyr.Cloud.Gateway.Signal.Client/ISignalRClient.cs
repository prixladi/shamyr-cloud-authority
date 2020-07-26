using System;
using System.Threading;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  public interface ISignalRClient: IAsyncDisposable
  {
    void ConnectAsync(CancellationToken cancellationToken);
  }
}