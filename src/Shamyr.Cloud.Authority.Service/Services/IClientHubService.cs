using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Service.Services
{
  public interface IClientHubService
  {
    Task SendEventAsync(UserEventBase @event, string method, IOperationContext context, CancellationToken cancellationToken);
  }
}
