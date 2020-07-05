using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Identity.Client.Models
{
  public class UserIdentityValidationModel
  {
    [Required]
    public IdentityResult Result { get; set; }

    public UserIdentityProfileModel? User { get; set; }
  }
}
