using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.Services
{
  public interface IClientHubService
  {
    Task SendEventAsync(IOperationContext context, IdentityUserEventMessageBase @event, string method, CancellationToken cancellationToken);
  }
}
