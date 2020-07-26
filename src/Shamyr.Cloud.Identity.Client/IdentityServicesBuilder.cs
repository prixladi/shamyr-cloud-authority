using System;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Identity.Client.Repositories;
using Shamyr.Cloud.Identity.Client.Services;
using Shamyr.Cloud.Identity.Client.SignalR;

namespace Shamyr.Cloud.Identity.Client
{
  public class IdentityServicesBuilder
  {
    private readonly IServiceCollection fServices;
    private readonly UserCacheServicesRepository fRepository;

    internal IdentityServicesBuilder(IServiceCollection services, UserCacheServicesRepository repository)
    {
      fServices = services ?? throw new ArgumentNullException(nameof(services));
      fRepository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IdentityServicesBuilder AddUserCacheService<TUserCache>(bool singleton = false)
      where TUserCache : class, IUserCacheService
    {
      if (singleton)
        fServices.AddSingleton<TUserCache>();
      else
        fServices.AddTransient<TUserCache>();

      fRepository.AddService<TUserCache>();
      return this;
    }

    public IdentityServicesBuilder AddGatewayEventClient()
    {
      fServices.AddGatewaySignalRClient<SignalRClientConfig, IdentityEventDispatcher>();
      return this;
    }
  }
}
