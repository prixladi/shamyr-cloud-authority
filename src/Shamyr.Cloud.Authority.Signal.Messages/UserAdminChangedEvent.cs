using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class UserAdminChangedEvent: UserEventBase
  {
    public override string Resource => Resources._UserDisabledChanged;

    public bool Admin { get; }

    [JsonConstructor]
    public UserAdminChangedEvent(string userId, bool admin, string operationId, string? parentOperationId)
      : base(userId, operationId, parentOperationId)
    {
      Admin = admin;
    }

    public UserAdminChangedEvent(string userId, bool admin, IOperationContext context)
      : base(userId, context)
    {
      Admin = admin;
    }
  }
}
