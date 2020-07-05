using System;
using System.Diagnostics;
using Shamyr.Cloud.Identity.Client.Models;

namespace Shamyr.Cloud.Identity.Client
{
  public static class ModelExtensions
  {
    public static UserIdentityValidationModel ToIdentityModel(this CachedUserModel cachedUserModel)
    {
      if (cachedUserModel is null)
        throw new ArgumentNullException(nameof(cachedUserModel));

      Debug.Assert(cachedUserModel.IdentityResult.HasValue);

      return new UserIdentityValidationModel
      {
        Result = cachedUserModel.IdentityResult.Value,
        User = cachedUserModel.Model
      };
    }

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

    public static CachedUserModel With(this CachedUserModel user, IdentityResult result)
    {
      if (user is null)
        throw new ArgumentNullException(nameof(user));

      return new CachedUserModel
      {
        Jwt = user.Jwt,
        IdentityResult = result,
        Model = user.Model
      };
    }
  }
}
