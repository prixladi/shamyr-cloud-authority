using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Database;
using Shamyr.Cloud.Gateway.Service.Repositories.Users;
using Shamyr.Cloud.Gateway.Service.Requests.UserPermissions;
using Shamyr.Cloud.Gateway.Service.Services.Identity;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.UserPermissions
{
  public class PutRequestHandler: IRequestHandler<PutRequest>
  {
    private readonly IUserPermissionRepository fUserPermissionRepository;
    private readonly IUserRepository fUserRepository;
    private readonly IIdentityService fIdentityService;

    public PutRequestHandler(
      IUserPermissionRepository userPermissionRepository,
      IUserRepository userRepository,
      IIdentityService identityService)
    {
      fUserPermissionRepository = userPermissionRepository;
      fUserRepository = userRepository;
      fIdentityService = identityService;
    }

    public async Task<Unit> Handle(PutRequest request, CancellationToken cancellationToken)
    {
      var currentUser = fIdentityService.Current;
      if (!await fUserRepository.ExistsAsync(request.UserId, cancellationToken))
        throw new NotFoundException($"User with ID '{request.UserId}' does not exist.");

      var settingPermission = (PermissionKind?)request.Model.Kind;
      var currentPermission = currentUser.UserPermissionDoc?.Kind;

      if (settingPermission.HasValue && settingPermission.Value >= currentPermission)
        throw new ForbiddenException($"Unable to alter user permission to permission '{settingPermission}' with only '{currentPermission}'.");

      var userPermission = await fUserPermissionRepository.GetAsync(request.UserId, cancellationToken);
      if (userPermission != null && userPermission.Kind >= currentPermission)
        throw new ForbiddenException($"Unable to alter user permission to user with permission '{userPermission.Kind}' with only '{currentPermission}'.");

      await fUserPermissionRepository.SetKindAsync(request.UserId, settingPermission, cancellationToken);

      return Unit.Value;
    }
  }
}
