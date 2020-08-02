using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Token.Models;

namespace Shamyr.Cloud.Authority.Client.Services
{
  public interface ITokenService
  {
    Task<UserModel?> TryGetUserAsync(string jwt, CancellationToken cancellationToken);
  }
}