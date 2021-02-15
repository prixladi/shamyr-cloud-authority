using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shamyr.Cloud.Authority.Service.Authentication.JwtBearer;
using Shamyr.Cloud.Authority.Service.Configs;
using Shamyr.Cloud.Authority.Service.IoC;
using Shamyr.Cloud.Authority.Service.SignalR.Hubs;
using Shamyr.Cloud.Authority.Signal.Messages;

namespace Shamyr.Cloud.Authority.Service
{
  public sealed class Startup
  {
    public static void ConfigureServices(IServiceCollection services)
    {
      services.AddExtensibleLogger();

      services.AddCors();
      services.AddOptions();
      services.AddControllers(MvcConfig.SetupMvcOptions)
        .AddJsonOptions(MvcConfig.SetupJsonOptions);

      services.AddSignalR()
        .AddJsonProtocol(SignalRConfig.SetupJsonProtocol);

      services.AddSwaggerGen(SwaggerConfig.SetupSwaggerGen);

      services.AddExceptionHandling();
      services.AddCustomExceptionHandling();

      services.AddDatabaseContext<DatabaseConfig, DatabaseOptions>();

      services.AddEmails();

      services.AddServiceAssembly();

      services.AddMediatR(typeof(Startup));

      services.AddAuthentication(AuthenticationConfig.SetupAuthentication)
        .AddJwtBearerAuthentication(AuthenticationConfig.SetupJwtBearer);

      services.AddAuthorization(AuthorizationConfig.Setup);
    }

    public static void Configure(IApplicationBuilder app)
    {
      app.UseSerilogRequestLogging(LoggingConfig.SetupRequests);
      
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
