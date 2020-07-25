using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Client.Services
{
  public interface IUserCacheService
  {
    Task<UserModel?> RetrieveUserAsync(string userId, CancellationToken cancellationToken);
    Task SaveUserAsync(string userId, UserModel userModel, CancellationToken cancellationToken);
    Task RemoveUserAsync(string userId, CancellationToken cancellationToken);
  }
}
