using Microsoft.AspNetCore.Authentication;
using Shamyr.AspNetCore.Authentication;

namespace Shamyr.Cloud.Identity.Service.Configs
{
  internal class AuthenticationConfig
  {
    public static void SetupAuthentication(AuthenticationOptions options)
    {
      options.DefaultAuthenticateScheme = Schemes._Basic;
      options.DefaultChallengeScheme = Schemes._Basic;
    }
  }
}
