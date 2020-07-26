using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;

namespace Shamyr.Cloud.Identity.Client.Handlers
{
  public abstract class IdentityEventHandlerBase<TMessage>: IIdentityEventHandler
    where TMessage : IdentityEventBase
  {
    public bool CanHandle(IdentityEventBase message)
    {
      return message is TMessage;
    }

    public async Task HandleAsync(IdentityEventBase message, CancellationToken cancellationToken)
    {
      await DoHandleAsync((TMessage)message, cancellationToken);
    }

    protected abstract Task DoHandleAsync(TMessage message, CancellationToken cancellationToken);
  }
}
