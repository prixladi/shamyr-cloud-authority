using Shamyr.Cloud.Identity.Client.Models;

namespace Shamyr.Cloud.Identity.Client
{
  // TODO: Rework this model, it's real pain to work with because of multiple api models
  public class CachedUserModel
  {
    public string? Jwt { get; set; }
    public IdentityResult? IdentityResult { get; set; }
    public UserIdentityProfileModel? Model { get; set; }
  }
}
