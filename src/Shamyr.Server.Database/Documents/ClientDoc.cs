using Shamyr.MongoDB;
using Shamyr.MongoDB.Attributes;
using Shamyr.MongoDB.Indexes;

namespace Shamyr.Server.Database.Documents
{
  [Collection(nameof(DbCollections.Clients))]
  public class ClientDoc: DocumentBase
  {
    [Indexed(Unique = true)]
    public string ClientId { get; set; } = default!;

    public SecretDoc Secret { get; set; } = default!;

    public bool Enabled { get; set; } = default!;
  }
}
