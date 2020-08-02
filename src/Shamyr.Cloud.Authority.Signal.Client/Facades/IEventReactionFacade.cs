using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Signal.Messages;

namespace Shamyr.Cloud.Authority.Client.Facades
{
  public interface IEventReactionFacade
  {
    Task ReactAsync(EventBase @event, CancellationToken cancellationToken);
  }
}