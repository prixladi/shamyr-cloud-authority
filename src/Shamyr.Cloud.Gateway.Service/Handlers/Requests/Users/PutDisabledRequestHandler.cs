using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Repositories.Users;
using Shamyr.Cloud.Gateway.Service.Requests.Users;
using Shamyr.Cloud.Gateway.Service.Services.Identity;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.Users
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
      if(user == null)
        throw new NotFoundException($"User with ID '{request.UserId}' does not exist.");

      if (user.Admin)
        throw new ForbiddenException($"User with ID '{request.UserId}' is admin.");

      await fUserRepository.TrySetDisabledAsync(request.UserId, request.Model.Disabled, cancellationToken);

      return Unit.Value;
    }
  }
}
