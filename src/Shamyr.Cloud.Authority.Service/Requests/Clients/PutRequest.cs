using System;
using MediatR;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Models.Clients;

namespace Shamyr.Cloud.Authority.Service.Requests.Clients
{
  public class PutRequest: IRequest
  {
    public ObjectId ClientId { get; }
    public ClientPutModel Model { get; }

    public PutRequest(ObjectId clientId, ClientPutModel model)
    {
      ClientId = clientId;
      Model = model ?? throw new ArgumentNullException(nameof(model));
    }
  }
}
