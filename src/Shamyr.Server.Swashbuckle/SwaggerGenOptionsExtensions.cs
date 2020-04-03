using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shamyr.Server.Swashbuckle
{
  public static class SwaggerGenOptionsExtensions
  {
    public const string _SecurityDefinitionName = "Identity";

    public static void AddIndentitySecurity(this SwaggerGenOptions options)
    {
      if (options is null)
        throw new ArgumentNullException(nameof(options));

      options.AddSecurityDefinition(_SecurityDefinitionName, new OpenApiSecurityScheme
      {
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        Description = "Identity authorization",
        In = ParameterLocation.Query,
        Scheme = "Identity"
      });

      options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = _SecurityDefinitionName}
          },
          Array.Empty<string>()
        }
      });
    }
  }
}
