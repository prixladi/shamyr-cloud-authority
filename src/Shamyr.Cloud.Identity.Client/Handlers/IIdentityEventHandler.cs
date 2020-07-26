using System;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Identity.Client.Handlers
{
  public interface IIdentityEventHandler
  {
    bool CanHandle(IdentityEventBase message);
    Task HandleAsync(IdentityEventBase message, CancellationToken cancellationToken);
  }
}
