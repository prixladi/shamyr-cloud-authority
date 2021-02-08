using Microsoft.AspNetCore.Authentication;
using Shamyr.Cloud.Authority.Client;
using Shamyr.Cloud.Authority.Client.Authentication;
using Shamyr.Cloud.Authority.Client.Factories;
using Shamyr.Cloud.Authority.Client.HostedServices;
using Shamyr.Cloud.Authority.Client.Repositories;
using Shamyr.Cloud.Authority.Client.Services;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class AuthenticationBuilderExtensions
  {
    public static AuthenticationBuilder AddAuthorityBearerAuthentication<TConfig, TPrincipalFactory>(this AuthenticationBuilder builder)
      where TConfig : class, IAuthorityClientConfig
      where TPrincipalFactory : PrincipalFactoryBase
    {
      builder.Services.AddTransient<IAuthorityClientConfig, TConfig>();
      builder.Services.AddTransient<IPrincipalFactory, TPrincipalFactory>();
      builder.Services.AddTransient<ITokenConfigurationService, TokenConfigurationService>();

      builder.Services.AddSingleton<ITokenService, TokenService>();
      builder.Services.AddSingleton<ITokenConfigurationRepository, TokenConfigurationRepository>();
      builder.Services.AddSingleton<IClaimsIdentityService, ClaimsIdentityService>();

      builder.Services.AddHostedService<TokenConfigurationCronService>();

      const string scheme = AuthorityAuthenticationDefaults._AuthenticationScheme;
      const string name = AuthorityAuthenticationDefaults._AuthenticationSchemeDisplayName;

      return builder.AddScheme<AuthenticationSchemeOptions, AuthorityAuthenticationHandler>(scheme, name, null);
    }
  }
}
