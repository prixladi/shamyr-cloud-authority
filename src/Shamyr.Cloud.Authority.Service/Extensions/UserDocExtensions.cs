using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Shamyr.Cloud.Authority.Service.Models.Users;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class UserDocExtensions
  {
    public static PreviewModel ToPreview(this UserDoc user)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new PreviewModel
      {
        Id = user.Id,
        Email = user.Email,
        Username = user.Username,
        Disabled = user.Disabled,
        GivenName = user.GivenName,
        FamilyName = user.FamilyName,
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
        GivenName = user.GivenName,
        FamilyName = user.FamilyName,
        Secret = user.Secret?.ToModel(),
        LogoutUtc = user.LogoutUtc,
        Admin = user.Admin
      };
    }

    public static IReadOnlyCollection<PreviewModel> ToPreview(this IEnumerable<UserDoc> users)
    {
      if (users is null)
        throw new ArgumentNullException(nameof(users));

      return users.Select(ToPreview).ToList();
    }

    public static DetailModel ToDetail(this UserDoc user)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new DetailModel
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
