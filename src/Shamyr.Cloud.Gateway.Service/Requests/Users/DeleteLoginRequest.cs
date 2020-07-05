using MediatR;
using MongoDB.Bson;

namespace Shamyr.Cloud.Gateway.Service.Requests.Users
{
  public class DeleteLoginRequest: IRequest
  {
    public ObjectId UserId { get; }

    public DeleteLoginRequest(ObjectId userId)
    {
      UserId = userId;
    }
  }
}
