using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client.Services
{
  public interface IIdentityService
  {
    Task<UserModel?> GetUserModelByIdAsync(string userId, IOperationContext context, CancellationToken cancellationToken);
  }
}
