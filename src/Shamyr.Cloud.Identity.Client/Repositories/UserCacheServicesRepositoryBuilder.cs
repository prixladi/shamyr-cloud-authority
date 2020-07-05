using System;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Identity.Client.SignalR;

namespace Shamyr.Cloud.Identity.Client.Repositories
{
  public class UserCacheServicesRepositoryBuilder
  {
    private readonly IServiceCollection fServices;
    private readonly UserCacheServicesRepository fRepository;

    internal UserCacheServicesRepositoryBuilder(IServiceCollection services, UserCacheServicesRepository repository)
    {
      fServices = services ?? throw new ArgumentNullException(nameof(services));
      fRepository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public UserCacheServicesRepositoryBuilder AddCacheService<TCache>(bool singleton = false)
      where TCache : class, IUserCacheService
    {
      if (singleton)
        fServices.AddSingleton<TCache>();
      else
        fServices.AddTransient<TCache>();

      fRepository.AddService<TCache>();

      return this;
    }

    public UserCacheServicesRepositoryBuilder AddGatewayEventClient<TConfig>()
      where TConfig : HubClientConfigBase
    {
      fServices.AddGatewayHubClient<TConfig, IdentityEventDispatcher>();

      return this;
    }
  }
}
