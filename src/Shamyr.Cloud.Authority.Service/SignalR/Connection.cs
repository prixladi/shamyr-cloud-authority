using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.SignalR
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
