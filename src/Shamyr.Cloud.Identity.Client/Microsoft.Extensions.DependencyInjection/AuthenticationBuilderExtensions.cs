using Microsoft.AspNetCore.Authentication;
using Shamyr.Cloud.Identity.Client.Authentication;
using Shamyr.Cloud.Identity.Client.Factories;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class AuthenticationBuilderExtensions
  {
    public static AuthenticationBuilder AddIdentityAuthentication<TPrincipalFactory>(this AuthenticationBuilder builder)
      where TPrincipalFactory : PrincipalFactoryBase
    {
      builder.Services.AddTransient<IPrincipalFactory, TPrincipalFactory>();

      return builder.AddScheme<IdentityAuthenticationOptions, IdentityAuthenticationHandler>
        (IdentityAuthenticationDefaults._AuthenticationScheme, "Identity authentication", null);
    }
  }
}
