using System;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Client.Reactions
{
  public class UserVerifiedChangedEventReaction: UserPropertyChangedEventReactionBase<UserVerifiedChangedEvent>
  {
    public UserVerifiedChangedEventReaction(IServiceProvider serviceProvider)
      : base(serviceProvider) { }

    protected override UserModel MutateUserAsync(UserModel model, UserVerifiedChangedEvent @event)
    {
      return model with { Verified = @event.Verified };
    }
  }
}
