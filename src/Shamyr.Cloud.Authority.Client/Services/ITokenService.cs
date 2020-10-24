using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Models;

namespace Shamyr.Cloud.Authority.Client.Services
{
  public interface ITokenService
  {
    Task<UserModel?> TryGetUserAsync(string jwt, CancellationToken cancellationToken);
  }
}