using System;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Client.Reactions
{
  public class UserDisabledChangedEventReaction: UserPropertyChangedEventReactionBase<UserDisabledChangedEvent>
  {
    public UserDisabledChangedEventReaction(IServiceProvider serviceProvider)
      : base(serviceProvider) { }

    protected override UserModel MutateUserAsync(UserModel model, UserDisabledChangedEvent @event)
    {
      return model with { Disabled = @event.Disabled };
    }
  }
}
