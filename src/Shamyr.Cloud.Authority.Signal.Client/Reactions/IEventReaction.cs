using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Signal.Messages;

namespace Shamyr.Cloud.Authority.Client.Reactions
{
  public interface IEventReaction
  {
    bool CanReact(EventBase @event);
    Task ReactAsync(EventBase @event, CancellationToken cancellationToken);
  }
}
