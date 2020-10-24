using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Token;
using Shamyr.Cloud.Authority.Service.Services.Identity;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Token
{
  public class LogoutRequestHandler: IRequestHandler<LogoutRequest>
  {
    private readonly IUserRepository fUserRepository;
    private readonly IIdentityService fIdentityService;

    public LogoutRequestHandler(IUserRepository userRepository, IIdentityService identityService)
    {
      fUserRepository = userRepository;
      fIdentityService = identityService;
    }

    public async Task<Unit> Handle(LogoutRequest request, CancellationToken cancellationToken)
    {
      var identity = fIdentityService.Current;

      await fUserRepository.LogoutAsync(identity.UserId, cancellationToken);

      return Unit.Value;
    }
  }
}
