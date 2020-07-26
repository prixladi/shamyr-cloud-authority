using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.AspNetCore.Hosting;
using Shamyr.Cloud.Identity.Client.Repositories;
using Shamyr.Cloud.Identity.Client.Services;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client.HostedServices
{
  public class TokenConfigurationCronService: CronServiceBase
  {
    private static readonly TimeSpan fInterval = TimeSpan.FromMinutes(10);

    public TokenConfigurationCronService(IServiceProvider provider)
      : base(fInterval, provider) { }

    protected override async Task ExecuteAsync(IServiceProvider provider, CancellationToken cancellationToken)
    {
      var configurationRepository = provider.GetRequiredService<ITokenConfigurationRepository>();
      var configurationService = provider.GetRequiredService<ITokenConfigurationService>();
      var config = provider.GetRequiredService<IIdentityClientConfig>();
      var tracker = provider.GetRequiredService<ITracker>();
      var context = OperationContext.Origin;

      var configuration = await configurationService!.TryGetAsync(config.GatewayUrl, context, cancellationToken);
      if (configuration is null)
      {
        tracker.TrackWarning(context, $"Unable to load gateway configuration. Signing key may be deprecated!");
        return;
      }

      configurationRepository.Set(configuration);
    }
  }
}
