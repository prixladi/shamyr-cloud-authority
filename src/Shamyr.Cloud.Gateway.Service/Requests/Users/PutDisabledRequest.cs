using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.Users;

namespace Shamyr.Cloud.Gateway.Service.Requests.Users
{
  public class PutDisabledRequest: IRequest
  {
    public ObjectId UserId { get; }
    public UserPatchDisabledModel Model { get; }

    public PutDisabledRequest(ObjectId userId, UserPatchDisabledModel model)
    {
      UserId = userId;
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
