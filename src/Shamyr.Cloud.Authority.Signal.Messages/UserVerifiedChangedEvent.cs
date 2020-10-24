using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class UserVerifiedChangedEvent: UserEventBase
  {
    public override string Resource => Resources._UserVerifiedChanged;

    public bool Verified { get; }

    [JsonConstructor]
    public UserVerifiedChangedEvent(string userId, bool verified, string scopeId, string? parentScopeId)
      : base(userId, scopeId, parentScopeId)
    {
      Verified = verified;
    }

    public UserVerifiedChangedEvent(string userId, bool verified, ILoggingContext context)
      : base(userId, context)
    {
      Verified = verified;
    }
  }
}
