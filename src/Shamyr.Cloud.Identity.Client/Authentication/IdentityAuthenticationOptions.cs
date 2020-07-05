using Microsoft.AspNetCore.Authentication;

namespace Shamyr.Cloud.Identity.Client.Authentication
{
  public class IdentityAuthenticationOptions: AuthenticationSchemeOptions
  {
    public string Challenge { get; set; } = IdentityAuthenticationDefaults._AuthenticationScheme;
  }
}
