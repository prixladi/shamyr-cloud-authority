using System;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Identity.Client.Handlers
{
  internal class UserVerificationStatusChangedEventHandler: UserIdentityEventHandlerBase<UserVerificationStatusChangedEvent>
  {
    public UserVerificationStatusChangedEventHandler(IServiceProvider serviceProvider)
      : base(serviceProvider) { }
  }
}
