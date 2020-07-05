using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages.Identity
{
  public class UserUserPermissionChangedEvent: IdentityUserEventMessageBase
  {
    public override string Resource => Resources._UserUserPermissionChanged;

    public PermissionKind? PermissionKind { get; }

    [JsonConstructor]
    public UserUserPermissionChangedEvent(string userId, PermissionKind? permissionKind, string operationId, string? parentOperationId)
      : base(userId, operationId, parentOperationId)
    {
      PermissionKind = permissionKind;
    }

    public UserUserPermissionChangedEvent(string userId, PermissionKind? permissionKind, IOperationContext context)
      : base(userId, context)
    {
      PermissionKind = permissionKind;
    }
  }
}
