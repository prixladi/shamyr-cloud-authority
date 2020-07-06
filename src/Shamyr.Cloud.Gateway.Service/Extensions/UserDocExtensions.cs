using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Models.Users;

namespace Shamyr.Cloud.Gateway.Service.Extensions
{
  public static class UserDocExtensions
  {
    public static UserPreviewModel ToModel(this UserDoc user)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new UserPreviewModel
      {
        Id = user.Id,
        Email = user.Email,
        Username = user.Username,
        Disabled = user.Disabled,
        Admin = user.Admin
      };
    }

    public static UserIdentityProfile ToIdentity(this UserDoc user, ClaimsIdentity identity)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new UserIdentityProfile(user.Id, identity)
      {
        Email = user.Email,
        Username = user.Username,
        Secret = user.Secret.ToModel(),
        LogoutUtc = user.LogoutUtc,
        Admin = user.Admin
      };
    }

    public static ICollection<UserPreviewModel> ToModel(this List<UserDoc> users)
    {
      if (users is null)
        throw new ArgumentNullException(nameof(users));

      return users.Select(ToModel).ToList();
    }

    public static UserDetailModel ToDetailModel(this UserDoc user)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new UserDetailModel
      {
        Id = user.Id,
        Email = user.Email,
        Username = user.Username,
        Disabled = user.Disabled,
        Admin = user.Admin
      };
    }
  }
}
