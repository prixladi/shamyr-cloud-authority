using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages.Identity
{
  public class UserVerificationStatusChangedEvent: IdentityUserEventBase
  {
    public override string Resource => Resources._UserVerificationStatusChanged;

    public bool Verified { get; }

    [JsonConstructor]
    public UserVerificationStatusChangedEvent(string userId, bool verified, string operationId, string? parentOperationId)
      : base(userId, operationId, parentOperationId)
    {
      Verified = verified;
    }

    public UserVerificationStatusChangedEvent(string userId, bool verified, IOperationContext context)
      : base(userId, context)
    {
      Verified = verified;
    }
  }
}
