using System;
using Shamyr.Cloud.Gateway.Service.Models.Users;

namespace Shamyr.Cloud.Gateway.Service.Extensions
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
