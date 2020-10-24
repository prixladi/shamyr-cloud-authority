using System;
using Shamyr.Cloud.Authority.Signal.Messages;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Client.Reactions
{
  public class UserAdminChangedEventReaction: UserPropertyChangedEventReactionBase<UserAdminChangedEvent>
  {
    public UserAdminChangedEventReaction(IServiceProvider serviceProvider)
      : base(serviceProvider) { }

    protected override UserModel MutateUserAsync(UserModel model, UserAdminChangedEvent @event)
    {
      return model with { Admin = @event.Admin };
    }
  }
}
