using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.Users;

namespace Shamyr.Cloud.Gateway.Service.Requests.Users
{
  public class GetRequest: IRequest<UserDetailModel>
  {
    public ObjectId Id { get; }

    public GetRequest(ObjectId id)
    {
      Id = id;
    }
  }
}
