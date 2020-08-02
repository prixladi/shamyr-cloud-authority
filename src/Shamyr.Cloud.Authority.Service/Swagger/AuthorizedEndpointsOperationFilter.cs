using System;
using Microsoft.OpenApi.Models;
using Shamyr.Cloud.Authority.Service.Configs;
using Shamyr.Cloud.Swashbuckle;

namespace Shamyr.Cloud.Authority.Service.Swagger
{
  public class AuthorizedEndpointsOperationFilter: AuthorizedEndpointsOperationFilterBase
  {
    protected override OpenApiSecurityRequirement? GetSecurityRequirement()
    {
      var scheme = new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = SwaggerConfig._SecurityDefinitionName
        }
      };

      return new OpenApiSecurityRequirement
      {
        [scheme] = Array.Empty<string>()
      };
    }
  }
}
