using System;
using Shamyr.Cloud.Authority.Service.Models.Users;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class UserIdentityProfileExtensions
  {
    public static DetailModel ToDetail(this UserIdentityProfile identity)
    {
      if (identity is null)
        throw new ArgumentNullException(nameof(identity));

      return new DetailModel
      {
        Id = identity.UserId,
        Email = identity.Email,
        Username = identity.Username,
        Admin = identity.Admin,
        GivenName = identity.GivenName,
        FamilyName = identity.FamilyName
      };
    }
  }
}
