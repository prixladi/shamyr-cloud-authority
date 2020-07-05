using System;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Identity.Client.Handlers
{
  public interface IUserIdentityEventHandler
  {
    bool CanHandle(IdentityUserEventMessageBase message);
    Task HandleAsync(IServiceProvider serviceProvider, IdentityUserEventMessageBase message, CancellationToken cancellationToken);
  }
}
