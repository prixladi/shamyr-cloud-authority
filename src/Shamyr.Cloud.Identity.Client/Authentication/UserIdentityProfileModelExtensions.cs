using System;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Client.Authentication
{
  public static class UserIdentityProfileModelExtensions
  {
    public static UserIdentity ToIdentity(this UserModel model)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      return new UserIdentity(id: model.Id.ToString(), username: model.Username, email: model.Email);
    }
  }
}
