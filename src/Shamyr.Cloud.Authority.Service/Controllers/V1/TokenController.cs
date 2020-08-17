using Microsoft.AspNetCore.Mvc;
using Shamyr.Cloud.Authority.Models;
using Shamyr.Cloud.Authority.Service.Configs;

namespace Shamyr.Cloud.Authority.Service.Controllers.V1
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

    [HttpGet("configuration")]
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
