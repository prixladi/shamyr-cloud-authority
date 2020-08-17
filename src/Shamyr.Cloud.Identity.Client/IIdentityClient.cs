using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Logging;

namespace Shamyr.Cloud.Identity.Client
{
  public interface IIdentityClient
  {
    Task<UserModel?> GetUserByIdAsync(string userId, ILoggingContext context, CancellationToken cancellationToken);
  }
}
