using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shamyr.DependencyInjection;

namespace Shamyr.Cloud.Gateway.Service.IoC
{
  internal static class ServiceAssembly
  {
    public static void AddServiceAssembly(this IServiceCollection services)
    {
      services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddSecretService();

      using var scan = services.CreateScanner<Startup>();
      scan.WithDefaultConventions();
    }
  }
}
