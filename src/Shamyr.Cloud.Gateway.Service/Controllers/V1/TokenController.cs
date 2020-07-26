﻿using Microsoft.AspNetCore.Mvc;
using Shamyr.Cloud.Gateway.Service.Configs;
using Shamyr.Cloud.Gateway.Token.Models;

namespace Shamyr.Cloud.Gateway.Service.Controllers.V1
{
  [ApiController]
  [Route("token")]
  public class ConfigurationController: ControllerBase
  {
    private readonly IJwtConfig fConfig;

    public ConfigurationController(IJwtConfig config)
    {
      fConfig = config;
    }

    [HttpGet("configuration.json")]
    public IActionResult Get()
    {
      return Ok
      (
        new TokenConfigurationModel
        {
          PublicKey = fConfig.BearerPublicKey,
          KeyDuration = fConfig.BearerTokenDuration,
          SignatureAlgorithm = "RS256",
          Issuer = fConfig.BearerTokenIssuer,
          Audience = fConfig.BearerTokenAudience
        }
      );
    }
  }
}