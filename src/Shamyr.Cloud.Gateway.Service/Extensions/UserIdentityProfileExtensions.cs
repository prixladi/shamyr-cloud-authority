using System;
using Shamyr.Cloud.Gateway.Service.Models.UserPermissions;
using Shamyr.Cloud.Gateway.Service.Models.Users;

namespace Shamyr.Cloud.Gateway.Service.Extensions
{
  public static class UserIdentityProfileExtensions
  {
    public static UserDetailModel ToModel(this UserIdentityProfile identity)
    {
      if (identity is null)
        throw new ArgumentNullException(nameof(identity));

      return new UserDetailModel
      {
        Id = identity.UserId,
        Email = identity.Email,
        Username = identity.Username,
        UserPermissionDoc = identity.UserPermissionDoc?.ToModel() ?? new PermissionDetailModel(),
      };
    }
  }
}
