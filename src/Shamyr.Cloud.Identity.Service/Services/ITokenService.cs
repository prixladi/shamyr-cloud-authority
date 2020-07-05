using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Service.Dtos;

namespace Shamyr.Cloud.Identity.Service.Services
{
  public interface ITokenService
  {
    Task<(IdentityResult, UserIdentityProfileDto?)> ValidateTokenAsync(string token, CancellationToken cancellationToken);
  }
}
