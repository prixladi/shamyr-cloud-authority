using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Gateway.Signal.Client
{
  public interface IIdentityEventDispatcher
  {
    public Task DispatchAsync(IdentityUserEventMessageBase @event, CancellationToken cancellation);
  }
}
