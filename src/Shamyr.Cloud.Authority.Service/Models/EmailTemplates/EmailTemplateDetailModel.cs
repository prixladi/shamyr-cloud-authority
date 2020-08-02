using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.EmailTemplates
{
  public class EmailTemplateDetailModel: EmailTemplatePreviewModel
  {
    [StringLength(int.MaxValue, MinimumLength = 1)]
    [Required]
    public string Body { get; set; } = default!;
  }
}
