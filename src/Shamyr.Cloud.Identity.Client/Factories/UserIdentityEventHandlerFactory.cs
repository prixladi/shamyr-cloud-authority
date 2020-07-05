using System;
using System.Collections.Generic;
using System.Linq;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Identity.Client.Handlers;
using Shamyr.DependencyInjection;

namespace Shamyr.Cloud.Identity.Client.Factories
{
  internal class UserIdentityEventHandlerFactory: FactoryBase<IUserIdentityEventHandler>, IUserIdentityEventHandlerFactory
  {
    public UserIdentityEventHandlerFactory(IServiceProvider serviceProvider)
      : base(serviceProvider) { }

    public IEnumerable<IUserIdentityEventHandler> Create(IdentityUserEventMessageBase message)
    {
      return GetComponents().Where(x => x.CanHandle(message));
    }
  }
}
