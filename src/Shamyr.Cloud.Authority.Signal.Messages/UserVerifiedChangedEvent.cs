using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class UserVerifiedChangedEvent: UserEventBase
  {
    public override string Resource => Resources._UserVerifiedChanged;

    public bool Verified { get; }

    [JsonConstructor]
    public UserVerifiedChangedEvent(string userId, bool verified, string operationId, string? parentOperationId)
      : base(userId, operationId, parentOperationId)
    {
      Verified = verified;
    }

    public UserVerifiedChangedEvent(string userId, bool verified, IOperationContext context)
      : base(userId, context)
    {
      Verified = verified;
    }
  }
}
