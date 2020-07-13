using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shamyr.Cloud.Gateway.Service.Authentication.JwtBearer;
using Shamyr.Cloud.Gateway.Service.Configs;
using Shamyr.Cloud.Gateway.Service.IoC;
using Shamyr.Cloud.Gateway.Service.SignalR.Hubs;
using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service
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

      services.AddSignalR()
        .AddJsonProtocol(SignalRConfig.SetupJsonProtocol);

      services.AddSwaggerGen(SwaggerConfig.SetupSwaggerGen);

      services.AddExceptionHandling();
      services.AddCustomExceptionHandling();

      services.AddDatabaseContext<DatabaseConfig>(DatabaseConfig.Setup);

      services.AddEmails();

      services.AddApplicationInsights(AppInsightsConfig.Setup);

      services.AddServiceAssembly();

      services.AddMediatR(typeof(Startup));

      services.AddAuthentication(AuthenticationConfig.SetupAuthentication)
        .AddJwtBearerAuthentication(AuthenticationConfig.SetupJwtBearer);

      services.AddAuthorization(AuthorizationConfig.Setup);
    }

    public static void Configure(IApplicationBuilder app)
    {
      app.UseCors(CorsConfig.Setup);

      app.UseExceptionHandling();

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseSwagger(SwaggerConfig.SetupSwagger);

      app.UseEndpoints(builder =>
      {
        builder.MapControllers();
        builder.MapHub<ClientHub>($"/{Routes._Client}");
      });
    }
  }
}
