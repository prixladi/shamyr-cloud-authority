using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using Shamyr.AspNetCore.Swashbuckle.Filters;
using Shamyr.Cloud.Gateway.Service.Swagger;
using Shamyr.Cloud.Swashbuckle.Bson;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shamyr.Cloud.Gateway.Service.Configs
{
  public static class SwaggerConfig
  {
    public const string _SecurityDefinitionName = "Bearer";

    private const string _V1Route = "v1";
    private const string _V1Title = "Shamyr Gateway Api";

    public static void SetupSwagger(SwaggerOptions options)
    {
      options.RouteTemplate = "docs/{documentName}/swagger.json";
    }

    public static void SetupSwaggerGen(SwaggerGenOptions options)
    {
      options.OperationFilter<AuthorizedEndpointsOperationFilter>();
      options.OperationFilter<OperationIdOperationFilter>();
      options.OperationFilter<FlattenObjectIdOperationFilter>();

      options.MapType<ObjectId>(() => new OpenApiSchema { Type = "string" });
      options.MapType<ObjectId?>(() => new OpenApiSchema { Type = "string" });
      options.MapType<byte>(() => new OpenApiSchema { Type = "integer" });

      options.SwaggerDoc(_V1Route, new OpenApiInfo { Title = _V1Title, Version = _V1Route });
      options.DocInclusionPredicate((version, apiDescription) => apiDescription.RelativePath.Contains($"/{version}/"));

      options.AddSecurityDefinition(_SecurityDefinitionName, new OpenApiSecurityScheme
      {
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        Description = "JWT Token authorization",
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

      foreach (string xmlFile in Directory.GetFiles(PlatformServices.Default.Application.ApplicationBasePath, "*.xml", SearchOption.AllDirectories))
        options.IncludeXmlComments(xmlFile);
    }
  }
}
