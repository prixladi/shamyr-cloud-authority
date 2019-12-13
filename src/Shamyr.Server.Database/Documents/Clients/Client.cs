using Shamyr.Database.Mongo;
using Shamyr.Database.Mongo.Attributes;

namespace Shamyr.Server.Database.Documents.Clients
{
  [MongoCollection(nameof(DbCollections.Users))]
  public class Client: DocumentBase
  {
    [Indexed(Unique = true)]
    public string ClientId { get; set; } = default!;

    public Secret Secret { get; set; } = default!;

    public bool Enabled { get; set; } = default!;
  }
}
