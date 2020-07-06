using System;
using Shamyr.Cloud.Identity.Service.Dtos;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Service.Extensions
{
  public static class UserDtoExtensions
  {
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
        Admin = user.Admin
      };
    }
  }
}
