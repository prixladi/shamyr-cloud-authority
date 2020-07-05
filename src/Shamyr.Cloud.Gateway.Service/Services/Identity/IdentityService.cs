using System;
using Microsoft.AspNetCore.Http;

namespace Shamyr.Cloud.Gateway.Service.Services.Identity
{
  public class IdentityService: IIdentityService
  {
    private readonly IHttpContextAccessor fHttpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
      fHttpContextAccessor = httpContextAccessor;
    }

    public UserIdentityProfile Current
    {
      get
      {
        var identity = (UserIdentityProfile?)fHttpContextAccessor.HttpContext.User.Identity;
        if (identity is null)
          throw new InvalidOperationException("User identity does not exist in current context.");

        return identity;
      }
    }
  }
}
