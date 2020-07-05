using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.Clients;

namespace Shamyr.Cloud.Gateway.Service.Requests.Clients
{
  public class GetRequest: IRequest<ClientDetailModel>
  {
    public ObjectId ClientId { get; }

    public GetRequest(ObjectId clientId)
    {
      ClientId = clientId;
    }
  }
}
