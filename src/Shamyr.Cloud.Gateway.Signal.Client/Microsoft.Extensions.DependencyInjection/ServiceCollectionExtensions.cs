using Shamyr.Cloud.Gateway.Signal.Client;
using Shamyr.Cloud.Gateway.Signal.Client.Services;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ServiceCollectionExtensions
  {
    public static void AddGatewayHubClient<TConfig, TEventDispatcher>(this IServiceCollection services)
      where TConfig : class, IHubClientConfig
      where TEventDispatcher : class, IIdentityEventDispatcher
    {
      services.AddTransient<IHubClientConfig, TConfig>();

      services.AddSingleton<IHubService, HubService>();
      services.AddSingleton<IGatewayHubClient, GatewayHubClient>();
      services.AddSingleton<IIdentityEventDispatcher, TEventDispatcher>();
      services.AddHostedService<SignalRClientManager>();
    }
  }
}
