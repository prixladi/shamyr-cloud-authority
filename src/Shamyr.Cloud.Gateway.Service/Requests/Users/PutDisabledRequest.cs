using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.Users;

namespace Shamyr.Cloud.Gateway.Service.Requests.Users
{
  public class PutDisabledRequest: IRequest
  {
    public ObjectId UserId { get; }
    public UserPutDisabledModel Model { get; }

    public PutDisabledRequest(ObjectId userId, UserPutDisabledModel model)
    {
      UserId = userId;
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
