using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Shamyr.Cloud.Identity.Client.Test
{
  public class UserCache: IUserCacheService
  {
    private readonly ConcurrentDictionary<string, CachedUserModel> fUsers;

    public UserCache()
    {
      fUsers = new ConcurrentDictionary<string, CachedUserModel>();
    }

    public Task ClearAync(CancellationToken cancellationToken)
    {
      fUsers.Clear();
      return Task.CompletedTask;
    }

    public Task RemoveUserAsync(string userId, CancellationToken cancellationToken)
    {
      fUsers.TryRemove(userId, out _);
      return Task.CompletedTask;
    }

    public Task<CachedUserModel?> RetrieveUserAsync(string userId, CancellationToken cancellationToken)
    {
      if (fUsers.TryGetValue(userId, out var model))
        return Task.FromResult<CachedUserModel?>(model);

      return Task.FromResult<CachedUserModel?>(null);
    }

    public Task SaveUserAsync(string userId, long expirationInSeconds, CachedUserModel userModel, CancellationToken cancellationToken)
    {
      fUsers.AddOrUpdate(userId, userModel, (_, __) => userModel);
      return Task.CompletedTask;
    }

    public Task SaveUserAsync(string userId, CachedUserModel userModel, CancellationToken cancellationToken)
    {
      fUsers.AddOrUpdate(userId, userModel, (_, __) => userModel);
      return Task.CompletedTask;
    }
  }
}
