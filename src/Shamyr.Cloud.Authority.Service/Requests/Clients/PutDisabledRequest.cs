using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Models.Clients;

namespace Shamyr.Cloud.Authority.Service.Requests.Clients
{
  public class PutDisabledRequest: IRequest
  {
    public ObjectId ClientId { get; }
    public PutDisabledModel Model { get; }

    public PutDisabledRequest(ObjectId clientId, PutDisabledModel model)
    {
      ClientId = clientId;
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
