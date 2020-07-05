using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Client.Models;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client
{
  public interface IIdentityClient
  {
    Task<UserIdentityProfileModel?> GetUserModelByIdAsync(IOperationContext context, string userId, CancellationToken cancellationToken);
    Task<UserIdentityValidationModel?> GetUserIdentityByJwtAsync(IOperationContext context, string token, CancellationToken cancellationToken);
  }
}
