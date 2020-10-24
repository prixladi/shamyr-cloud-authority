using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Client.Repositories;
using Shamyr.Cloud.Authority.Signal.Messages;

namespace Shamyr.Cloud.Authority.Client.Reactions
{
  public class TokenConfigurationChangedEventReaction: IEventReaction
  {
    private readonly ITokenConfigurationRepository fTokenConfigurationRepository;

    public TokenConfigurationChangedEventReaction(ITokenConfigurationRepository tokenConfigurationRepository)
    {
      fTokenConfigurationRepository = tokenConfigurationRepository;
    }

    public bool CanReact(EventBase @event)
    {
      return @event is TokenConfigurationChangedEvent;
    }

    public Task ReactAsync(EventBase @event, CancellationToken cancellationToken)
    {
      var tokenEvent = (TokenConfigurationChangedEvent)@event;
      fTokenConfigurationRepository.Set(tokenEvent.Model);
      return Task.CompletedTask;
    }
  }
}
