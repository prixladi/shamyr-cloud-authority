namespace Shamyr.Cloud.Identity.Service.Models
{
  public class UserIdentityValidationModel
  {
    public IdentityResult Result { get; set; }
    public UserIdentityProfileModel? User { get; set; }
  }
}
