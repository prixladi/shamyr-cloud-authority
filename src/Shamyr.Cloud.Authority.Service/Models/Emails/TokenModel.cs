using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Emails
{
  public record TokenModel
  {
    [Required]
    public string Token { get; set; } = default!;
  }
}
