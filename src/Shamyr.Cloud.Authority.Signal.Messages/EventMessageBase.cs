using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public abstract class EventMessageBase: EventBase
  {
    protected EventMessageBase(string operationId, string? parentOperationId)
      : base(operationId, parentOperationId) { }

    protected EventMessageBase(IOperationContext context)
      : base(context) { }
  }
}
