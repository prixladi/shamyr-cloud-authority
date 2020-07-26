using System;
using System.Collections.Generic;
using System.Linq;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Identity.Client.Handlers;
using Shamyr.DependencyInjection;

namespace Shamyr.Cloud.Identity.Client.Factories
{
  internal class IdentityEventHandlerFactory: FactoryBase<IIdentityEventHandler>, IIdentityEventHandlerFactory
  {
    public IdentityEventHandlerFactory(IServiceProvider serviceProvider)
      : base(serviceProvider) { }

    public IEnumerable<IIdentityEventHandler> Create(IdentityEventBase message)
    {
      return GetComponents().Where(x => x.CanHandle(message));
    }
  }
}
