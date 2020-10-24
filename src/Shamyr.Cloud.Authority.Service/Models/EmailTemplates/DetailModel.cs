using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Models.EmailTemplates
{
  public record DetailModel
  {
    [Required]
    public ObjectId Id { get; init; }

    [StringLength(25, MinimumLength = 1)]
    [Required]
    public string Name { get; init; } = default!;

    [StringLength(50, MinimumLength = 1)]
    [Required]
    public string Subject { get; init; } = default!;

    public bool IsHtml { get; init; }

    [EnumDataType(typeof(EmailTemplateType))]
    public EmailTemplateType Type { get; init; }

    [StringLength(int.MaxValue, MinimumLength = 1)]
    [Required]
    public string Body { get; init; } = default!;
  }
}
