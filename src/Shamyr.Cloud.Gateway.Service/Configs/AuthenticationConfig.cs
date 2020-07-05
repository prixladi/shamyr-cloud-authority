using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public static class AuthenticationConfig
  {
    public static void SetupAuthentication(AuthenticationOptions options)
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    public static void SetupJwtBearer(JwtBearerOptions options)
    {
      var config = new JwtConfig();

      options.RequireHttpsMetadata = false;
      options.SaveToken = false;
      options.Events = new JwtBearerEvents
      {
        OnMessageReceived = context =>
        {
          string authorization = context.Request.Headers["Authorization"];
          if (authorization != null && authorization.StartsWith("Bearer "))
            context.Token = authorization.Substring("Bearer ".Length);

          return Task.CompletedTask;
        }
      };
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidIssuer = config.BearerTokenIssuer,
        ValidAudience = config.BearerTokenAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.BearerTokenSymetricKey)),
        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role
      };
    }
  }
}
