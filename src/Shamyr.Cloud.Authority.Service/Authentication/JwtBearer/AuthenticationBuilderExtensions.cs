using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Shamyr.Cloud.Authority.Service.Authentication.JwtBearer
{
  public static class AuthenticationBuilderExtensions
  {
    public static AuthenticationBuilder AddJwtBearerAuthentication(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
    {
      return builder
        .AddScheme<JwtBearerOptions, JwtBearerAuthenticationHandler>
        (JwtBearerDefaults.AuthenticationScheme, "JWT Bearer", configureOptions);
    }
  }
}
