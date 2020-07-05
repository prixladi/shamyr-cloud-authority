using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages
{
  public abstract class EventMessageBase: MessageBase
  {
    protected EventMessageBase(string operationId, string? parentOperationId)
      : base(operationId, parentOperationId) { }

    protected EventMessageBase(IOperationContext context)
      : base(context) { }
  }
}
