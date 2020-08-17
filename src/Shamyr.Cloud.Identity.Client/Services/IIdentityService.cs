using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Logging;

namespace Shamyr.Cloud.Identity.Client.Services
{
  public interface IIdentityService
  {
    Task<UserModel?> GetUserModelByIdAsync(string userId, ILoggingContext context, CancellationToken cancellationToken);
  }
}
