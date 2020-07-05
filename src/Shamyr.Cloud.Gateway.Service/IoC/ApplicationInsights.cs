using System;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.AspNetCore.Services;

namespace Shamyr.Cloud.Gateway.Service.IoC
{
  public static class ApplicationInsights
  {
    public static void AddApplicationInsights(this IServiceCollection services, Action<ApplicationInsightsServiceOptions> setupOptions)
    {
      services.AddApplicationInsightsTelemetry(setupOptions);
      services.AddApplicationInsightsTracker(RoleNames._GatewayService);

      services.AddTransient<ITelemetryService, TelemetryService>();
    }
  }
}
