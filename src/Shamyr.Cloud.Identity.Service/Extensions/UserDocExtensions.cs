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
        Admin = user.Admin,
        Verified = user.EmailToken is null
      };
    }

    public static UserModel ToDetail(this UserDoc user)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new UserModel
      {
        Id = user.Id.ToString(),
        Username = user.Username,
        Email = user.Email,
        Disabled = user.Disabled,
        GivenName = user.GivenName,
        FamilyName = user.FamilyName,
        Verified = user.EmailToken is null,
        Admin = user.Admin
      };
    }
  }
}
