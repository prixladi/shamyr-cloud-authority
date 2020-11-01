using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Authority.Client.Authentication;

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

      services.AddIdentityServices<IdentityClientConfig>()
        .AddIdentityCaching<CachePipelineFactory>()
        .AddUserCacheService<UserCache>(true)
        .AddAuthorityEventClient();

      services.AddSwaggerGen(SwaggerConfig.SetupSwaggerGen);

      services.AddApplicationInsightsTelemetry();
      services.AddApplicationInsightsLogger("Identity test");

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = AuthorityAuthenticationDefaults._AuthenticationScheme;
        options.DefaultChallengeScheme = AuthorityAuthenticationDefaults._AuthenticationScheme;
      })
      .AddAuthorityBearerAuthentication<IdentityClientConfig, PrincipalFactory>();
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
