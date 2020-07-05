using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.SignalR
{
  public class ConnectionContext: OperationContext, IConnectionContext
  {
    public Connection Connection { get; }

    public ConnectionContext(IOperationContext context, Connection connection)
      : base(context.Id, context.ParentId)
    {
      Connection = connection;
    }
  }
}
