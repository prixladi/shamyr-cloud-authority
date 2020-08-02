using System;
using Shamyr.AspNetCore.Services;
using Shamyr.Cloud.Identity.Client;
using Shamyr.Cloud.Identity.Client.Repositories;
using Shamyr.Cloud.Identity.Client.Services;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ServiceCollectionExtensions
  {
    public static IdentityServicesBuilder AddIdentityServices<TConfig>(this IServiceCollection services)
      where TConfig : class, IIdentityClientConfig
    {
      if (services is null)
        throw new ArgumentNullException(nameof(services));

      services.AddTransient<IIdentityClientConfig, TConfig>();
      services.AddTransient<IIdentityClient, IdentityRestClient>();
      services.AddTransient<IIdentityService, IdentityService>();
      services.AddTransient<ITelemetryService, TelemetryService>();

      services.AddSingleton<ITokenConfigurationRepository, TokenConfigurationRepository>();

      return new IdentityServicesBuilder(services);
    }
  }
}
