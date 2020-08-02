using System;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Authority.Client.Reactions;
using Shamyr.Cloud.Identity.Client.Services;
using Shamyr.Cloud.Identity.Client.SignalR;
using Shamyr.Extensions.DependencyInjection;

namespace Shamyr.Cloud.Identity.Client
{
  public class IdentityCacheBuilder
  {
    private readonly IServiceCollection fServices;

    internal IdentityCacheBuilder(IServiceCollection services)
    {
      fServices = services ?? throw new ArgumentNullException(nameof(services));
    }

    public IdentityCacheBuilder AddUserCacheService<TUserCache>(bool singleton = false)
      where TUserCache : class, IUserCacheService
    {
      if (singleton)
        fServices.AddSingleton<TUserCache>();
      else
        fServices.AddScoped<TUserCache>();

      return this;
    }

    public IdentityCacheBuilder AddAuthorityEventClient()
    {
      fServices.AddAuthoritySignalRClient<SignalRClientConfig>();
      fServices.AddAllTypesOf<IEventReaction>(typeof(IdentityCacheBuilder).Assembly);
      return this;
    }
  }
}
