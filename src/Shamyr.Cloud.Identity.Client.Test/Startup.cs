﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Identity.Client.Authentication;

namespace Shamyr.Cloud.Identity.Client.Test
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.AddIdentity<IdentityClientConfig>()
        .AddUserCacheService<UserCache>(true)
        .AddGatewayEventClient();

      services.AddSwaggerGen(SwaggerConfig.SetupSwaggerGen);

      services.AddApplicationInsightsTelemetry();
      services.AddApplicationInsightsTracker("Identity test");

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = IdentityAuthenticationDefaults._AuthenticationScheme;
        options.DefaultChallengeScheme = IdentityAuthenticationDefaults._AuthenticationScheme;
      })
      .AddIdentityAuthentication<PrincipalFactory>();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseCors(builder =>
      {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
      });

      app.UseMiddleware<ExceptionMiddleware>();

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseSwagger(SwaggerConfig.SetupSwagger);
    }
  }
}
