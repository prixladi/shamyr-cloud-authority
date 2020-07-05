using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.SignalR
{
  public interface IConnectionContext: IOperationContext
  {
    Connection Connection { get; }
  }
}