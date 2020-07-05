using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.UserPermissions;
using Shamyr.Cloud.Gateway.Service.Repositories.Users;
using Shamyr.Cloud.Gateway.Service.Requests.UserPermissions;
using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.UserPermissions
{
  public class GetRequestHandler: IRequestHandler<GetRequest, PermissionDetailModel>
  {
    private readonly IUserPermissionRepository fUserPermissionRepository;
    private readonly IUserRepository fUserRepository;

    public GetRequestHandler(IUserPermissionRepository userPermissionRepository, IUserRepository userRepository)
    {
      fUserPermissionRepository = userPermissionRepository;
      fUserRepository = userRepository;
    }

    public async Task<PermissionDetailModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
      await UserExistsOrThrowAsync(request.UserId, cancellationToken);

      var permission = await fUserPermissionRepository.GetAsync(request.UserId, cancellationToken);

      return new PermissionDetailModel
      {
        Kind = (PermissionKind)permission.Kind
      };
    }

    private async Task UserExistsOrThrowAsync(ObjectId userId, CancellationToken cancellationToken)
    {
      if (!await fUserRepository.ExistsAsync(userId, cancellationToken))
        throw new NotFoundException($"User with ID '{userId}' does not exist.");
    }
  }
}
