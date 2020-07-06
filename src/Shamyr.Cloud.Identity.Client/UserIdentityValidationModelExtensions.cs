using System;
using Shamyr.Cloud.Identity.Client.Models;

namespace Shamyr.Cloud.Identity.Client
{
  public static class UserIdentityValidationModelExtensions
  {
    public static CachedUserModel ToCachedUserModel(this UserIdentityValidationModel identityModel, string jwt)
    {
      if (identityModel is null)
        throw new ArgumentNullException(nameof(identityModel));

      if (jwt is null)
        throw new ArgumentNullException(nameof(jwt));

      return new CachedUserModel
      {
        Jwt = jwt,
        IdentityResult = identityModel.Result,
        Model = identityModel.User
      };
    }
  }
}
