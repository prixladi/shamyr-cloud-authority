using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Logins;
using Shamyr.Cloud.Authority.Service.Services.Identity;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Logins
{
  public class DeleteRequestHandler: IRequestHandler<DeleteRequest>
  {
    private readonly IUserRepository fUserRepository;
    private readonly IIdentityService fIdentityService;

    public DeleteRequestHandler(IUserRepository userRepository, IIdentityService identityService)
    {
      fUserRepository = userRepository;
      fIdentityService = identityService;
    }

    public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken)
    {
      var identity = fIdentityService.Current;

      var scewedTime = DateTime.UtcNow.Add(TimeSpan.FromSeconds(-1));
      await fUserRepository.LogoutAsync(identity.UserId, scewedTime, cancellationToken);

      return Unit.Value;
    }
  }
}
