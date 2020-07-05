using System.Threading;
using System.Threading.Tasks;

namespace Shamyr.Cloud.Identity.Client
{
  public interface IUserCacheService
  {
    Task<CachedUserModel?> RetrieveUserAsync(string userId, CancellationToken cancellationToken);
    Task SaveUserAsync(string userId, long expirationInSeconds, CachedUserModel userModel, CancellationToken cancellationToken);
    Task SaveUserAsync(string userId, CachedUserModel userModel, CancellationToken cancellationToken);
    Task RemoveUserAsync(string userId, CancellationToken cancellationToken);
    Task ClearAync(CancellationToken cancellationToken);
  }
}
