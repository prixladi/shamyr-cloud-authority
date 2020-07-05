using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Client.Models;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client
{
  public interface IIdentityRemoteClient
  {
    Task<UserIdentityValidationModel> GetIdentityByJwtAsync(IOperationContext context, string token, CancellationToken cancellationToken);
    Task<UserIdentityProfileModel?> GetUserByIdAsync(IOperationContext context, string userId, CancellationToken cancellationToken);
  }
}
