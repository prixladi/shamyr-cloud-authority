using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shamyr.Cloud.Authority.Client.SignalR
{
  public interface ISignalRClient: IAsyncDisposable
  {
    Task Connect(CancellationToken cancellationToken);
  }
}