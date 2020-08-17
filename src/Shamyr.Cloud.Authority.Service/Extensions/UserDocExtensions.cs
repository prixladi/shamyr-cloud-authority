using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Authority.Service.Models.Users;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class UserDocExtensions
  {
    public static UserPreviewModel ToPreview(this UserDoc user)
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
        Secret = user.Secret?.ToModel(),
        LogoutUtc = user.LogoutUtc,
        Admin = user.Admin
      };
    }

    public static IReadOnlyCollection<UserPreviewModel> ToPreview(this IEnumerable<UserDoc> users)
    {
      if (users is null)
        throw new ArgumentNullException(nameof(users));

      return users.Select(ToPreview).ToList();
    }

    public static UserDetailModel ToDetail(this UserDoc user)
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
