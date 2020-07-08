using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.Users;

namespace Shamyr.Cloud.Gateway.Service.Requests.Users
{
  public class PutAdminRequest: IRequest
  {
    public ObjectId UserId { get; }
    public UserPutAdminModel Model { get; }

    public PutAdminRequest(ObjectId userId, UserPutAdminModel model)
    {
      UserId = userId;
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
