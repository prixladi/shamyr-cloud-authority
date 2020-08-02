using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Models.Users;

namespace Shamyr.Cloud.Authority.Service.Requests.Users
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
