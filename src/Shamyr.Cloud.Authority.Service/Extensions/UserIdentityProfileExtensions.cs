using System;
using Shamyr.Cloud.Authority.Service.Models.Users;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class UserIdentityProfileExtensions
  {
    public static UserDetailModel ToDetail(this UserIdentityProfile identity)
    {
      if (identity is null)
        throw new ArgumentNullException(nameof(identity));

      return new UserDetailModel
      {
        Id = identity.UserId,
        Email = identity.Email,
        Username = identity.Username,
        Admin = identity.Admin
      };
    }
  }
}
