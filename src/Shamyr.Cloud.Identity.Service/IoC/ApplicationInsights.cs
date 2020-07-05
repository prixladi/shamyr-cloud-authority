using System;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Shamyr.Cloud.Identity.Service.IoC
{
  public static class ApplicationInsights
  {
    public static void AddApplicationInsights(this IServiceCollection services, Action<ApplicationInsightsServiceOptions> options)
    {
      services.AddApplicationInsightsTelemetry(options);
      services.AddApplicationInsightsTracker("Identity Service");
    }
  }
}
