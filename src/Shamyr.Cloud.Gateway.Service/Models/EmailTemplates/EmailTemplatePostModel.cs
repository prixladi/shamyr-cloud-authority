using System.ComponentModel.DataAnnotations;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Gateway.Service.Models.EmailTemplates
{
  public class EmailTemplatePostModel
  {
    [StringLength(25, MinimumLength = 1)]
    [Required]
    public string Name { get; set; } = default!;

    [StringLength(50, MinimumLength = 1)]
    [Required]
    public string Subject { get; set; } = default!;

    [StringLength(int.MaxValue, MinimumLength = 1)]
    [Required]
    public string Body { get; set; } = default!;

    public bool IsHtml { get; set; }

    public EmailTemplateType Type { get; set; }
  }
}
