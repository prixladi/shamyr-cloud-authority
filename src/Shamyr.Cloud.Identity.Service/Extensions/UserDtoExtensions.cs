using System;
using Shamyr.Cloud.Identity.Service.Dtos;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Cloud.Identity.Service.Protos;

namespace Shamyr.Cloud.Identity.Service.Extensions
{
  public static class UserDtoExtensions
  {
    public static UserIdentityProfileMessage ToMessage(this UserIdentityProfileDto user)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new UserIdentityProfileMessage
      {
        Id = user.Id.ToString(),
        Username = user.Username,
        Email = user.Email,
        Disabled = user.Disabled,
        PermissionKind = PermissionKindExtensions.ToMessageKind(user.PermissionKind),
      };
    }

    public static UserIdentityProfileModel ToModel(this UserIdentityProfileDto user)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new UserIdentityProfileModel
      {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        Disabled = user.Disabled,
        UserPermission = new UserPermissionModel { Kind = user.PermissionKind },
      };
    }
  }
}
