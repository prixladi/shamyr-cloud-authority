using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Users;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Users
{
  public class DeleteLoginRequestHandler: IRequestHandler<DeleteLoginRequest>
  {
    private readonly IUserRepository fUserRepository;

    public DeleteLoginRequestHandler(IUserRepository userRepository) 
    {
      fUserRepository = userRepository;
    }

    public async Task<Unit> Handle(DeleteLoginRequest request, CancellationToken cancellationToken)
    {
      await fUserRepository.LogoutAsync(request.UserId, cancellationToken);

      return Unit.Value;
    }
  }
}
