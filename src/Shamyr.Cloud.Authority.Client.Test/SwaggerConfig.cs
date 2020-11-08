using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Shamyr.Cloud.Swashbuckle;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shamyr.Cloud.Authority.Client.Test
{
  using SwaggerGenOptionsExtensions = Swashbuckle.SwaggerGenOptionsExtensions;

  public static class SwaggerConfig
  {
    private class AuthFilter: AuthorizedEndpointsOperationFilterBase
    {
      protected override OpenApiSecurityRequirement? GetSecurityRequirement()
      {
        var scheme = new OpenApiSecurityScheme
        {
          Reference = new OpenApiReference
          {
            Type = ReferenceType.SecurityScheme,
            Id = SwaggerGenOptionsExtensions._SecurityDefinitionName
          }
        };

        return new OpenApiSecurityRequirement
        {
          [scheme] = Array.Empty<string>()
        };
      }
    }

    private const string _V1Route = "v1";
    private const string _V1Title = "Shamyr Cloud Test Api";

    public static void SetupSwagger(SwaggerOptions options)
    {
      options.RouteTemplate = "docs/{documentName}/swagger.json";
    }

    public static void SetupSwaggerGen(SwaggerGenOptions options)
    {
      options.OperationFilter<AuthFilter>();
      options.AddIndentitySecurity();

      options.SwaggerDoc(_V1Route, new OpenApiInfo { Title = _V1Title, Version = _V1Route });
      options.DocInclusionPredicate((version, apiDescription) => apiDescription.RelativePath.Contains($"/{version}/"));

      foreach (string xmlFile in Directory.GetFiles(PlatformServices.Default.Application.ApplicationBasePath, "*.xml", SearchOption.AllDirectories))
        options.IncludeXmlComments(xmlFile);
    }
  }
}
