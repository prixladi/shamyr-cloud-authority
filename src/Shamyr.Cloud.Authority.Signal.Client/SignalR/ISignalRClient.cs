using System;
using System.Threading;

namespace Shamyr.Cloud.Authority.Client.SignalR
{
  public interface ISignalRClient: IAsyncDisposable
  {
    void ConnectAsync(CancellationToken cancellationToken);
  }
}