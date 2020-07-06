using Microsoft.Extensions.DependencyInjection;
using Shamyr.DependencyInjection;

namespace Shamyr.Cloud.Identity.Service.IoC
{
  internal static class ServiceAssembly
  {
    public static void AddServiceAssembly(this IServiceCollection services)
    {
      using var scan = services.CreateScanner<Startup>();
      scan.WithDefaultConventions();
    }
  }
}
