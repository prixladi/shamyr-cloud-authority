using System;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Models.UserPermissions;
using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service.Extensions
{
  public static class UserPermissionDocExtensions
  {
    public static PermissionDetailModel ToModel(this UserPermissionDoc permission)
    {
      if (permission is null)
        throw new ArgumentNullException(nameof(permission));

      return new PermissionDetailModel
      {
        Kind = (PermissionKind)permission.Kind
      };
    }
  }
}
