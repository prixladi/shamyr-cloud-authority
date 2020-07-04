using Shamyr.MongoDB;
using Shamyr.MongoDB.Attributes;
using Shamyr.MongoDB.Indexes;

namespace Shamyr.Server.Database.Documents
{
  [Collection(nameof(DbCollections.Clients))]
  public class ClientDoc: DocumentBase
  {
    [Index(Unique = true)]
    public string ClientName { get; set; } = default!;

    public SecretDoc Secret { get; set; } = default!;

    public bool Disabled { get; set; } = default!;
  }
}
