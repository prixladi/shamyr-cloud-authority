using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Service.Services
{
  public interface IClientHubService
  {
    Task SendEventAsync(UserEventBase @event, string method, ILoggingContext context, CancellationToken cancellationToken);
  }
}
