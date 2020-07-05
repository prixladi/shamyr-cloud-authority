using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Models.Clients;

namespace Shamyr.Cloud.Gateway.Service.Requests.Clients
{
  public class PatchDisabledRequest: IRequest
  {
    public ObjectId ClientId { get; }
    public ClientPatchDisabledModel Model { get; }

    public PatchDisabledRequest(ObjectId clientId, ClientPatchDisabledModel model)
    {
      ClientId = clientId;
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
