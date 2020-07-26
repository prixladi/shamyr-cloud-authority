using System;
using Shamyr.AspNetCore.Services;
using Shamyr.Cloud.Identity.Client;
using Shamyr.Cloud.Identity.Client.Factories;
using Shamyr.Cloud.Identity.Client.Handlers;
using Shamyr.Cloud.Identity.Client.HostedServices;
using Shamyr.Cloud.Identity.Client.Repositories;
using Shamyr.Cloud.Identity.Client.Services;
using Shamyr.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ServiceCollectionExtensions
  {
    public static IdentityServicesBuilder AddIdentity<TConfig>(this IServiceCollection services)
      where TConfig : class, IIdentityClientConfig
    {
      if (services is null)
        throw new ArgumentNullException(nameof(services));

      services.AddTransient<IIdentityClientConfig, TConfig>();
      services.AddTransient<IIdentityClient, IdentityRestClient>();

      services.AddTransient<IIdentityService, IdentityService>();
      services.AddTransient<ITokenConfigurationService, TokenConfigurationService>();
      services.AddTransient<ITelemetryService, TelemetryService>();

      services.AddSingleton<ITokenConfigurationRepository, TokenConfigurationRepository>();

      services.AddHostedService<TokenConfigurationCronService>();

      using (var scan = services.CreateScanner<IIdentityEventHandler>())
        scan.AddAllTypesOf<IIdentityEventHandler>();

      services.AddTransient<IIdentityEventHandlerFactory, IdentityEventHandlerFactory>();

      var cacheRepository = new UserCacheServicesRepository();
      services.AddSingleton<IUserCacheServicesRepository>(cacheRepository);

      return new IdentityServicesBuilder(services, cacheRepository);
    }
  }
}
