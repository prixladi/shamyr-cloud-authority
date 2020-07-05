using MongoDB.Bson;

namespace Shamyr.Cloud.Gateway.Service.Models
{
  public class IdModel
  {
    public ObjectId Id { get; }

    public IdModel(ObjectId id)
    {
      Id = id;
    }
  }
}
