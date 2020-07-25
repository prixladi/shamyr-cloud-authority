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
    /// <summary>
    /// Adds 'IIdentityClient' to your dependency bootstrap
    /// </summary>
    /// <param name="services"></param>
    /// <param name="identityClientConfig"></param>
    public static UserCacheServicesRepositoryBuilder AddIdentity<TConfig>(this IServiceCollection services)
      where TConfig : class, IIdentityClientConfig
    {
      if (services is null)
        throw new ArgumentNullException(nameof(services));

      services.AddTransient<IIdentityClientConfig, TConfig>();
      services.AddTransient<IIdentityClient, IdentityRestClient>();

      services.AddTransient<IIdentityService, IdentityService>();
      services.AddTransient<ITokenConfigurationService, TokenConfigurationService>();
      services.AddTransient<ITelemetryService, TelemetryService>();

      services.AddSingleton<IUserLockRepository, UserLockRepository>();
      services.AddSingleton<ITokenConfigurationRepository, TokenConfigurationRepository>();

      services.AddHostedService<TokenConfigurationCronService>();

      using (var scan = services.CreateScanner<IUserIdentityEventHandler>())
        scan.AddAllTypesOf<IUserIdentityEventHandler>();

      services.AddTransient<IUserIdentityEventHandlerFactory, UserIdentityEventHandlerFactory>();

      var cacheRepository = new UserCacheServicesRepository();
      services.AddSingleton<IUserCacheServicesRepository>(cacheRepository);

      return new UserCacheServicesRepositoryBuilder(services, cacheRepository);
    }
  }
}
