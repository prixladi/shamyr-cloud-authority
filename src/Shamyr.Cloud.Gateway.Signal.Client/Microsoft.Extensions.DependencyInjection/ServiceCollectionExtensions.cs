using Shamyr.Cloud.Gateway.Signal.Client;
using Shamyr.Cloud.Gateway.Signal.Client.Services;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ServiceCollectionExtensions
  {
    public static void AddGatewaySignalRClient<TConfig, TEventDispatcher>(this IServiceCollection services)
      where TConfig : class, ISignalRClientConfig
      where TEventDispatcher : class, IIdentityEventDispatcher
    {
      services.AddTransient<ISignalRClientConfig, TConfig>();

      services.AddSingleton<IHubService, HubService>();
      services.AddSingleton<ISignalRClient, SignalRClient>();
      services.AddSingleton<IIdentityEventDispatcher, TEventDispatcher>();
      services.AddHostedService<SignalRClientManager>();
    }
  }
}
