using System;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Identity.Client.Services;
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

    public UserCacheServicesRepositoryBuilder AddUserCacheService<TUserCache>(bool singleton = false)
      where TUserCache : class, IUserCacheService
    {
      if (singleton)
        fServices.AddSingleton<TUserCache>();
      else
        fServices.AddTransient<TUserCache>();

      fRepository.AddService<TUserCache>();
      return this;
    }

    public UserCacheServicesRepositoryBuilder AddGatewayEventClient()
    {
      fServices.AddGatewayHubClient<HubClientConfig, IdentityEventDispatcher>();
      return this;
    }
  }
}
