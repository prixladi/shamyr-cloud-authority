using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Identity.Client.Repositories;

namespace Shamyr.Cloud.Identity.Client.Handlers
{
  public class UserLoggedOutEventHandler: UserIdentityEventHandlerBase<UserLoggedOutEvent>
  {
    public UserLoggedOutEventHandler(
      IUserCacheServicesRepository userCacheServicesRepository,
      IUserLockRepository userLockRepository)
      : base(userCacheServicesRepository, userLockRepository) { }
  }
}
