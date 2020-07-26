using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Gateway.Signal.Messages
{
  public interface IRemoteClient
  {
    Task UserLoggedOutEventAsync(UserLoggedOutEvent @event);
    Task UserVerificationStatusChangedEventAsync(UserVerificationStatusChangedEvent @event);
    Task TokenConfigurationChangedAsync(TokenConfigurationChangedEvent @event);
  }
}
