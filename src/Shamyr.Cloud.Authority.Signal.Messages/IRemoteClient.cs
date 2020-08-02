using System.Threading.Tasks;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public interface IRemoteClient
  {
    Task UserLoggedOutEventAsync(UserLoggedOutEvent @event);
    Task UserVerifiedChangedEventAsync(UserVerifiedChangedEvent @event);
    Task UserDisabledChangedEventAsync(UserDisabledChangedEvent  @event);
    Task UserAdminChangedEventAsync(UserAdminChangedEvent @event);
    Task TokenConfigurationChangedAsync(TokenConfigurationChangedEvent @event);
  }
}
