using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Client.Services
{
  public interface ITokenService
  {
    TIdentity GetCurrentUserIdentity<TIdentity>() where TIdentity : ClaimsIdentity;
    Task<UserModel?> TryGetUserAsync(string jwt, CancellationToken cancellationToken);
  }
}