using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Models.Clients;

namespace Shamyr.Cloud.Authority.Service.Requests.Clients
{
  public class GetRequest: IRequest<DetailModel>
  {
    public ObjectId ClientId { get; }

    public GetRequest(ObjectId clientId)
    {
      ClientId = clientId;
    }
  }
}
