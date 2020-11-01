using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Shamyr.Cloud.Database.Documents;

using static Shamyr.Cloud.Authority.Service.Models.ModelConstants;

namespace Shamyr.Cloud.Authority.Service.Models.EmailTemplates
{
  public record PutModel
  {
    [StringLength(_MaxTemplateNameLength, MinimumLength = _MinTemplateNameLength)]
    [Required]
    public string Name { get; init; } = default!;

    [StringLength(_MaxTemplateSubjectLength, MinimumLength = _MinTemplateSubjectLength)]
    [Required]
    public string Subject { get; init; } = default!;

    [StringLength(_MaxTemplateBodyLength, MinimumLength = _MinTemplateBodyLength)]
    [Required]
    public string Body { get; init; } = default!;

    public bool IsHtml { get; init; }

    public EmailTemplateType Type { get; init; }
  }
}
