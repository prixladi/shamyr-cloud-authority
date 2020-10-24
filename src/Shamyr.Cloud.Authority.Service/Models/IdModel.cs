using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Models
{
  public record IdModel
  {
    public ObjectId Id { get; init; }
  }
}
