using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Shamyr.Cloud.Identity.Client.Repositories;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Identity.Client.Services
{
  internal class CacheInitService: IHostedService
  {
    private readonly IServiceProvider fServiceProvider;
    private readonly IUserCacheServicesRepository fUserCacheServicesRepository;
    private readonly ITracker fTracker;

    public CacheInitService(
      IServiceProvider serviceProvider,
      IUserCacheServicesRepository userCacheServicesRepository,
      ITracker tracker)
    {
      fServiceProvider = serviceProvider;
      fUserCacheServicesRepository = userCacheServicesRepository;
      fTracker = tracker;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      fTracker.TrackInformation(OperationContext.Origin, "Initilizing identity client cache.");

      // TODO: What about multiple instances?
      foreach (var service in fUserCacheServicesRepository.GetCacheServices(fServiceProvider))
        await service.ClearAync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}
