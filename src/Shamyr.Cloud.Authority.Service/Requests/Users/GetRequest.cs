using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Models.Users;

namespace Shamyr.Cloud.Authority.Service.Requests.Users
{
  public class GetRequest: IRequest<DetailModel>
  {
    public ObjectId Id { get; }

    public GetRequest(ObjectId id)
    {
      Id = id;
    }
  }
}
