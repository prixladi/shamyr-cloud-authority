using System;

using static Shamyr.Cloud.Identity.Service.Protos.GetByTokenReply.Types;

namespace Shamyr.Cloud.Identity.Client
{
  public static class IdentityResultExtensions
  {
    public static Models.IdentityResult ToModel(this IdentityResult result)
    {
      return result switch
      {
        IdentityResult.Ok => Models.IdentityResult.Ok,
        IdentityResult.Invalid => Models.IdentityResult.Invalid,
        IdentityResult.LoggedOut => Models.IdentityResult.LoggedOut,
        IdentityResult.NotVerified => Models.IdentityResult.NotVerified,
        IdentityResult.Disabled => Models.IdentityResult.Disabled,
        IdentityResult.NotFound => Models.IdentityResult.NotFound,
        _ => throw new NotImplementedException(result.ToString())
      };
    }
  }
}
