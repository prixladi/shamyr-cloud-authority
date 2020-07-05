using System.Collections.Generic;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Identity.Client.Handlers;

namespace Shamyr.Cloud.Identity.Client.Factories
{
  public interface IUserIdentityEventHandlerFactory
  {
    IEnumerable<IUserIdentityEventHandler> Create(IdentityUserEventMessageBase message);
  }
}