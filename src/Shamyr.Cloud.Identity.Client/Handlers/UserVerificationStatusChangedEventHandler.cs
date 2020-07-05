using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Identity.Client.Repositories;

namespace Shamyr.Cloud.Identity.Client.Handlers
{
  public class UserVerificationStatusChangedEventHandler: UserIdentityEventHandlerBase<UserVerificationStatusChangedEvent>
  {
    public UserVerificationStatusChangedEventHandler(
      IUserCacheServicesRepository userCacheServicesRepository,
      IUserLockRepository userLockRepository)
      : base(userCacheServicesRepository, userLockRepository) { }
  }
}
