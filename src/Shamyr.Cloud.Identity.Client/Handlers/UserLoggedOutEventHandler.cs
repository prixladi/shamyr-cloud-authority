using System;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Identity.Client.Handlers
{
  internal class UserLoggedOutEventHandler: UserIdentityEventHandlerBase<UserLoggedOutEvent>
  {
    public UserLoggedOutEventHandler(IServiceProvider serviceProvider)
      : base(serviceProvider) { }
  }
}
