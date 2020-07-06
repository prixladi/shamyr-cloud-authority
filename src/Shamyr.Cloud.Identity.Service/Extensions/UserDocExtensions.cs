using System;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Identity.Service.Dtos;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Service.Extensions
{
  public static class UserDocExtensions
  {
    public static UserIdentityProfileDto ToDto(this UserDoc user)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new UserIdentityProfileDto
      {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        Disabled = user.Disabled,
        PermissionKind = user.UserPermission?.Kind
      };
    }

    public static UserIdentityProfileModel ToModel(this UserDoc user)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new UserIdentityProfileModel
      {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        Disabled = user.Disabled,
        UserPermission = user.UserPermission?.ToModel() ?? new UserPermissionModel()
      };
    }
  }
}
