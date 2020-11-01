using Shamyr.Cloud.Authority.Client.Facades;
using Shamyr.Cloud.Authority.Client.Factories;
using Shamyr.Cloud.Authority.Client.Reactions;
using Shamyr.Cloud.Authority.Client.Services;
using Shamyr.Cloud.Authority.Client.SignalR;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ServiceCollectionExtensions
  {
    public static void AddAuthoritySignalRClient<TConfig>(this IServiceCollection services)
      where TConfig : class, IAuthoritySignalRClientConfig
    {
      services.AddTransient<IAuthoritySignalRClientConfig, TConfig>();
      services.AddTransient<IHubConnectionService, HubConnectionService>();
      services.AddTransient<IEventReactionFactory, EventReactionFactory>();
      services.AddTransient<IEventReactionFacade, EventReactionFacade>();

      services.AddTransient<IEventReaction, TokenConfigurationChangedEventReaction>();

      services.AddSingleton<ISignalRClient, SignalRClient>();

      services.AddHostedService<SignalRClientStarterService>();
    }
  }
}
