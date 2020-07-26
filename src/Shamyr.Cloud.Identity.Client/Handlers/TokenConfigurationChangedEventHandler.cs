using System;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Identity.Client.Repositories;

namespace Shamyr.Cloud.Identity.Client.Handlers
{
  public class TokenConfigurationChangedEventHandler: IIdentityEventHandler
  {
    private readonly ITokenConfigurationRepository fTokenConfigurationRepository;

    public TokenConfigurationChangedEventHandler(ITokenConfigurationRepository tokenConfigurationRepository)
    {
      fTokenConfigurationRepository = tokenConfigurationRepository;
    }

    public bool CanHandle(IdentityEventBase message)
    {
      return message is TokenConfigurationChangedEvent;
    }

    public Task HandleAsync(IdentityEventBase message, CancellationToken cancellationToken)
    {
      var configMessage = (TokenConfigurationChangedEvent)message;
      fTokenConfigurationRepository.Set(configMessage.Model);
      return Task.CompletedTask;
    }
  }
}
