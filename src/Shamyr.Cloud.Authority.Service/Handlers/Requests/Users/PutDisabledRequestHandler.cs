using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Users;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Users
{
  public class PutDisabledRequestHandler: IRequestHandler<PutDisabledRequest>
  {
    private readonly IUserRepository fUserRepository;

    public PutDisabledRequestHandler(IUserRepository userRepository)
    {
      fUserRepository = userRepository;
    }

    public async Task<Unit> Handle(PutDisabledRequest request, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetAsync(request.UserId, cancellationToken);
      if (user is null)
        throw new NotFoundException($"User with ID '{request.UserId}' does not exist.");
      if (user.Admin)
        throw new ForbiddenException($"User with ID '{request.UserId}' is admin.");

      await fUserRepository.TrySetDisabledAsync(request.UserId, request.Model.Disabled, cancellationToken);

      return Unit.Value;
    }
  }
}
