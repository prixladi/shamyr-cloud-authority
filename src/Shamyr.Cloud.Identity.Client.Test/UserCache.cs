using System.Collections.Concurrent;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Client.Services;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Client.Test
{
  public class UserCache: IUserCacheService
  {
    private readonly ConcurrentDictionary<string, UserModel> fUsers;

    public UserCache()
    {
      fUsers = new ConcurrentDictionary<string, UserModel>();
    }

    public Task RemoveUserAsync(string userId, CancellationToken cancellationToken)
    {
      fUsers.TryRemove(userId, out _);
      return Task.CompletedTask;
    }

    public Task<UserModel?> RetrieveUserAsync(string userId, CancellationToken cancellationToken)
    {
      if (fUsers.TryGetValue(userId, out var model))
        return Task.FromResult<UserModel?>(model);

      return Task.FromResult<UserModel?>(null);
    }

    public Task SaveUserAsync(string userId, UserModel userModel, CancellationToken cancellationToken)
    {
      fUsers.AddOrUpdate(userId, userModel, (_, __) => userModel);
      return Task.CompletedTask;
    }
  }
}
