using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.UserPermissions;

namespace Shamyr.Cloud.Gateway.Service.Requests.UserPermissions
{
  public class PutRequest: IRequest
  {
    public ObjectId UserId { get; }
    public PermissionPatchModel Model { get; }

    public PutRequest(ObjectId userId, PermissionPatchModel model)
    {
      UserId = userId;
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
