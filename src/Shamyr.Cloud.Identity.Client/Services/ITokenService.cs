using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Client.Services
{
  public interface ITokenService
  {
    Task<UserModel?> TryGetUserAsync(string jwt, CancellationToken cancellationToken);
  }
}