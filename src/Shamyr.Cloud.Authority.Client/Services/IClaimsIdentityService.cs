using System.Security.Claims;

namespace Shamyr.Cloud.Authority.Client.Services
{
  public interface IClaimsIdentityService
  {
    TIdentity GetCurrentUser<TIdentity>() where TIdentity : ClaimsIdentity;
    TIdentity? TryGetCurrentUser<TIdentity>() where TIdentity : ClaimsIdentity;
  }
}