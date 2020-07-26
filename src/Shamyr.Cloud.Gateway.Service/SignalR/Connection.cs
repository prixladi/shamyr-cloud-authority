using MongoDB.Bson;

namespace Shamyr.Cloud.Gateway.Service.SignalR
{
  public class Connection
  {
    public ObjectId? ClientId { get; }

    public Connection(ObjectId? clientId = null)
    {
      ClientId = clientId;
    }
  }
}
