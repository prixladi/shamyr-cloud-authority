using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages.Identity
{
  public abstract class IdentityEventBase: EventMessageBase
  {
    protected IdentityEventBase(string operationId, string? parentOperationId)
      : base(operationId, parentOperationId) { }

    protected IdentityEventBase(IOperationContext context)
      : base(context) { }
  }
}
