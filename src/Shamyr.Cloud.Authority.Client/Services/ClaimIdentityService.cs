using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Shamyr.Cloud.Authority.Client.Services
{
  internal class ClaimsIdentityService: IClaimsIdentityService
  {
    private readonly IHttpContextAccessor fHttpContextAccessor;

    public ClaimsIdentityService(IHttpContextAccessor httpContextAccessor)
    {
      fHttpContextAccessor = httpContextAccessor;
    }

    public TIdentity GetCurrentUser<TIdentity>()
      where TIdentity : ClaimsIdentity
    {
      var identity = TryGetCurrentUser<TIdentity>();
      return identity ?? throw new InvalidOperationException("Unable to retrieve user identity in current context.");
    }

    public TIdentity? TryGetCurrentUser<TIdentity>()
      where TIdentity : ClaimsIdentity
    {
      return (TIdentity?)fHttpContextAccessor.HttpContext.User.Identity;
    }
  }
}
