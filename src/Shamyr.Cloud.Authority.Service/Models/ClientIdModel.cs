using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Models
{
  public record ClientIdModel
  {
    public ObjectId ClientId { get; init; }
  }
}
