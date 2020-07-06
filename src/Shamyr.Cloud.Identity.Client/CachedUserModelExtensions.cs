using System;
using System.Diagnostics;
using Shamyr.Cloud.Identity.Client.Models;

namespace Shamyr.Cloud.Identity.Client
{
  public static class CachedUserModelExtensions
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
