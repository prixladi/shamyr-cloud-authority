using System;
using Shamyr.Cloud.Identity.Client.Models;
using Shamyr.Cloud.Identity.Service.Protos;

using static Shamyr.Cloud.Identity.Service.Protos.UserIdentityProfileMessage.Types;

namespace Shamyr.Cloud.Identity.Client.Extensions
{
  public static class MessagesExtensions
  {
    public static UserIdentityProfileModel ToModel(this UserIdentityProfileMessage message)
    {
      if (message is null)
        throw new ArgumentNullException(nameof(message));

      return new UserIdentityProfileModel
      {
        Id = message.Id,
        Username = message.Username,
        Email = message.Email,
        Disabled = message.Disabled,
        UserPermission = message.PermissionKind.ToPermission()
      };
    }

    public static UserPermissionModel ToPermission(this PermissionKind kind)
    {
      return kind switch
      {
        PermissionKind.View => new UserPermissionModel { Kind = Gateway.Signal.Messages.PermissionKind.View },
        PermissionKind.Control => new UserPermissionModel { Kind = Gateway.Signal.Messages.PermissionKind.Control },
        PermissionKind.Configure => new UserPermissionModel { Kind = Gateway.Signal.Messages.PermissionKind.Configure },
        PermissionKind.Own => new UserPermissionModel { Kind = Gateway.Signal.Messages.PermissionKind.Own },
        PermissionKind.None => new UserPermissionModel(),
        _ => throw new NotImplementedException(kind.ToString())
      };
    }
  }
}
