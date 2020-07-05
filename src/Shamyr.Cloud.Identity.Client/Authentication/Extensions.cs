using System;
using Shamyr.Cloud.Identity.Client.Models;

namespace Shamyr.Cloud.Identity.Client.Authentication
{
  public static class Extensions
  {
    public static UserIdentity ToIdentity(this UserIdentityProfileModel model)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      return new UserIdentity(id: model.Id.ToString(), username: model.Username, email: model.Email);
    }
  }
}
