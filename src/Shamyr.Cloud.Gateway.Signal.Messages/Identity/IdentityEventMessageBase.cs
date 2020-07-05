using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages.Identity
{
  public abstract class IdentityEventMessageBase: EventMessageBase
  {
    [JsonIgnore]
    public abstract string Resource { get; }

    protected IdentityEventMessageBase(string operationId, string? parentOperationId)
      : base(operationId, parentOperationId) { }

    protected IdentityEventMessageBase(IOperationContext context)
      : base(context) { }
  }
}
