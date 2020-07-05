using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.Users;

namespace Shamyr.Cloud.Gateway.Service.Requests.Users
{
  public class PatchPasswordResetRequest: IRequest
  {
    public ObjectId UserId { get; }
    public UserPatchPasswordModel Model { get; }

    public PatchPasswordResetRequest(ObjectId userId, UserPatchPasswordModel model)
    {
      UserId = userId;
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
