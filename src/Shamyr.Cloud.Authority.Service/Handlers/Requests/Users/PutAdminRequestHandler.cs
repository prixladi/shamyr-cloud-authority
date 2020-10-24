using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Users;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Users
{
  public class PutAdminRequestHandler: IRequestHandler<PutAdminRequest>
  {
    private readonly IUserRepository fUserRepository;

    public PutAdminRequestHandler(IUserRepository userRepository)
    {
      fUserRepository = userRepository;
    }

    public async Task<Unit> Handle(PutAdminRequest request, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetAsync(request.UserId, cancellationToken);
      if (user is null)
        throw new NotFoundException($"User with ID '{request.UserId}' does not exist.");
      if (user.Admin)
        throw new ForbiddenException($"User with ID '{request.UserId}' is admin.");

      await fUserRepository.TrySetAdminAsync(request.UserId, request.Model.Admin, cancellationToken);

      return Unit.Value;
    }
  }
}
