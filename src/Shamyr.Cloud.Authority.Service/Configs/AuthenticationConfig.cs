using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shamyr.Cloud.Authority.Models;
using Shamyr.Security.IdentityModel;

namespace Shamyr.Cloud.Authority.Service.Configs
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
      options.MapInboundClaims = false;

      options.Events = new JwtBearerEvents
      {
        OnMessageReceived = context =>
        {
          string authorization = context.Request.Headers["Authorization"];
          if (authorization is not null && authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            context.Token = authorization["Bearer ".Length..];

          return Task.CompletedTask;
        }
      };

      var rsa = RSA.Create();
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidIssuer = config.BearerTokenIssuer,
        ValidAudience = config.BearerTokenAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5),
        IssuerSigningKey = rsa.ToSecurityKey(config.BearerPublicKey, true),
        NameClaimType = Constants._NameClaim,
        RoleClaimType = Constants._RoleClaim
      };
    }
  }
}
