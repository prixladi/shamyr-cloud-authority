using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Gateway.Signal.Messages
{
  public interface IRemoteClient
  {
    Task UserLoggedOutEventAsync(UserLoggedOutEvent @event);
    Task UserUserPermissionChangedEventAsync(UserUserPermissionChangedEvent @event);
    Task UserVerificationStatusChangedEventAsync(UserVerificationStatusChangedEvent @event);
  }
}
