using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.UserPermissions;

namespace Shamyr.Cloud.Gateway.Service.Requests.UserPermissions
{
  public class GetRequest: IRequest<PermissionDetailModel>
  {
    public ObjectId UserId { get; }

    public GetRequest(ObjectId userId)
    {
      UserId = userId;
    }
  }
}
