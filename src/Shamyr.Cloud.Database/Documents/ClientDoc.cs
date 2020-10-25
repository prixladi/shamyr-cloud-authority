using System;
using MongoDB.Bson;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Attributes;
using Shamyr.MongoDB.Indexes;

namespace Shamyr.Cloud.Database.Documents
{
  [Collection(nameof(DbCollections.Clients))]
  public class ClientDoc: DocumentBase
  {
    [Index(Unique = true)]
    public string Name { get; set; } = default!;
    public SecretDoc? Secret { get; set; }
    public ObjectId? VerifyAccountEmailTemplateId { get; set; }
    public ObjectId? PasswordResetEmailTemplateId { get; set; }
    public string? AuthorityUrl { get; set; }
    public string? PortalUrl { get; set; }
    public bool Disabled { get; set; }
  }
}
