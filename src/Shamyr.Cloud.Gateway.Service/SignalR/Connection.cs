using MongoDB.Bson;

namespace Shamyr.Cloud.Gateway.Service.SignalR
{
  public class Connection
  {
    public string Id { get; }
    public ObjectId? ClientId { get; }

    public Connection With(ObjectId clientId)
    {
      return new Connection(Id, clientId);
    }

    public Connection(string id, ObjectId? clientId = null)
    {
      Id = id;
      ClientId = clientId;
    }
  }
}
