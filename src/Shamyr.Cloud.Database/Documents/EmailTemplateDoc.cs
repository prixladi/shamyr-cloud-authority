using Shamyr.MongoDB;
using Shamyr.MongoDB.Attributes;
using Shamyr.MongoDB.Indexes;

namespace Shamyr.Cloud.Database.Documents
{
  [Collection(nameof(DbCollections.EmailTemplates))]
  public class EmailTemplateDoc: DocumentBase
  {
    public string Name { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;
    public bool IsHtml { get; set; }
    
    [Index(Unique = true)]
    public EmailTemplateType Type { get; set; }
  }
}
