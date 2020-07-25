using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client
{
  public interface IIdentityClient
  {
    Task<UserModel?> GetUserByIdAsync(string userId, IOperationContext context, CancellationToken cancellationToken);
  }
}
