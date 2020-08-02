using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shamyr.Cloud.Swashbuckle
{
  public static class SwaggerGenOptionsExtensions
  {
    public const string _SecurityDefinitionName = "Bearer";

    public static void AddIndentitySecurity(this SwaggerGenOptions options)
    {
      if (options is null)
        throw new ArgumentNullException(nameof(options));

      options.AddSecurityDefinition(_SecurityDefinitionName, new OpenApiSecurityScheme
      {
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        Description = "Identity authorization",
        In = ParameterLocation.Header,
        Scheme = _SecurityDefinitionName
      });

      options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Name = _SecurityDefinitionName,
            In = ParameterLocation.Header,
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = _SecurityDefinitionName}
          },
          Array.Empty<string>()
        }
      });
    }
  }
}
