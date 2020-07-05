using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Shamyr.Cloud.Gateway.Service.Authentication.JwtBearer
{
  public static class JwtBearerAuthenticationExtensions
  {
    public static AuthenticationBuilder AddJwtBearerAuthentication(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
    {
      return builder
        .AddScheme<JwtBearerOptions, JwtBearerAuthenticationHandler>
        (JwtBearerDefaults.AuthenticationScheme, "JWT Bearer", configureOptions);
    }
  }
}
