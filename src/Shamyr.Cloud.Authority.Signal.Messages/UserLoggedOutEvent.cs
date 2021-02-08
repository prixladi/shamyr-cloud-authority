using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class UserLoggedOutEvent: UserEventBase
  {
    public override string Resource => Resources._UserLoggedOut;

    [JsonConstructor]
    public UserLoggedOutEvent(string userId, string scopeId, string? parentScopeId)
      : base(userId, scopeId, parentScopeId) { }

    public UserLoggedOutEvent(string userId, ILoggingContext operationContext)
      : base(userId, operationContext) { }
  }
}
