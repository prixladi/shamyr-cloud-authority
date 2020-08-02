using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shamyr.Extensions.DependencyInjection;

namespace Shamyr.Cloud.Authority.Service.IoC
{
  internal static class ServiceAssembly
  {
    public static void AddServiceAssembly(this IServiceCollection services)
    {
      services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddSecretService();

      services.AddDefaultConventions<Startup>();
    }
  }
}
