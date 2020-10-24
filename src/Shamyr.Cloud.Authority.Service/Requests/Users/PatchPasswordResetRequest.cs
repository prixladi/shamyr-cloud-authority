using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Models.Users;

namespace Shamyr.Cloud.Authority.Service.Requests.Users
{
  public class PatchPasswordResetRequest: IRequest
  {
    public ObjectId UserId { get; }
    public PatchPasswordModel Model { get; }

    public PatchPasswordResetRequest(ObjectId userId, PatchPasswordModel model)
    {
      UserId = userId;
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
