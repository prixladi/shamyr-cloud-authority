using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class UserAdminChangedEvent: UserEventBase
  {
    public override string Resource => Resources._UserDisabledChanged;

    public bool Admin { get; }

    [JsonConstructor]
    public UserAdminChangedEvent(string userId, bool admin, string scopeId, string? parentScopeId)
      : base(userId, scopeId, parentScopeId)
    {
      Admin = admin;
    }

    public UserAdminChangedEvent(string userId, bool admin, ILoggingContext context)
      : base(userId, context)
    {
      Admin = admin;
    }
  }
}
