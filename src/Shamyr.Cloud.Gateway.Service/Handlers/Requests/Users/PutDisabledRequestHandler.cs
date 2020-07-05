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
    private readonly IUserPermissionRepository fUserPermissionRepository;
    private readonly IIdentityService fIdentityService;

    public PutDisabledRequestHandler(
      IUserRepository userRepository,
      IUserPermissionRepository userPermissionRepository,
      IIdentityService identityService)
    {
      fUserRepository = userRepository;
      fUserPermissionRepository = userPermissionRepository;
      fIdentityService = identityService;
    }

    public async Task<Unit> Handle(PutDisabledRequest request, CancellationToken cancellationToken)
    {
      var currentUser = fIdentityService.Current;

      var userPermission = await fUserPermissionRepository.GetAsync(request.UserId, cancellationToken);

      if (userPermission != null && userPermission.Kind >= currentUser.UserPermissionDoc?.Kind)
        throw new ForbiddenException($"User with id '{request.UserId}' has greater or equal permission than current user ({userPermission.Kind}).");

      if (!await fUserRepository.TrySetDisabledAsync(request.UserId, request.Model.Disabled, cancellationToken))
        throw new NotFoundException($"User with ID '{request.UserId}' does not exist.");

      return Unit.Value;
    }
  }
}
