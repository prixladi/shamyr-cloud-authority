using System.Collections.Generic;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Identity.Client.Handlers;

namespace Shamyr.Cloud.Identity.Client.Factories
{
  public interface IIdentityEventHandlerFactory
  {
    IEnumerable<IIdentityEventHandler> Create(IdentityEventBase message);
  }
}