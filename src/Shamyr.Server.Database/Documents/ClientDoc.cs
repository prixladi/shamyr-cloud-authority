using Shamyr.Database.Mongo;
using Shamyr.Database.Mongo.Attributes;

namespace Shamyr.Server.Database.Documents
{
  [MongoCollection(nameof(DbCollections.Clients))]
  public class ClientDoc: DocumentBase
  {
    [Indexed(Unique = true)]
    public string ClientId { get; set; } = default!;

    public SecretDoc Secret { get; set; } = default!;

    public bool Enabled { get; set; } = default!;
  }
}
