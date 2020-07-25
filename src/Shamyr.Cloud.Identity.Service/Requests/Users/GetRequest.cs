using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Identity.Service.Models;

namespace Shamyr.Cloud.Identity.Service.Requests.Users
{
  public class GetRequest: IRequest<UserModel>
  {
    public ObjectId UserId { get; }

    public GetRequest(ObjectId userId)
    {
      UserId = userId;
    }
  }
}
