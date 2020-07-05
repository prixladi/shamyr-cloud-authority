using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shamyr.Cloud.Identity.Service.Authentication;
using Shamyr.Cloud.Identity.Service.Configs;
using Shamyr.Cloud.Identity.Service.IoC;

namespace Shamyr.Cloud.Identity.Service
{
  public sealed class Startup
  {
    public static void ConfigureServices(IServiceCollection services)
    {
      services.AddLogging(LoggingConfig.Setup);
      services.AddCors();
      services.AddOptions();
      services.AddControllers(MvcConfig.SetupMvcOptions)
        .AddJsonOptions(MvcConfig.SetupJsonOptions);

      services.AddAuthentication(AuthenticationConfig.SetupAuthentication)
        .AddBasicAuthentication<BasicAuthenticationHandler>();

      services.AddExceptionHandling();
      services.AddDatabaseContext<DatabaseConfig>();
      services.AddApplicationInsights(AppInsightsConfig.Setup);

      services.AddSecretService();
      services.AddServiceAssembly();
    }

    public static void Configure(IApplicationBuilder app)
    {
      app.UseCors(CorsConfig.Setup);

      app.UseExceptionHandling();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(builder =>
      {
        builder.MapControllers();
      });
    }
  }
}
