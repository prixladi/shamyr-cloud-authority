using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Configs;
using Shamyr.Security;
using Shamyr.Security.Tokens.Jwt;

namespace Shamyr.Cloud.Gateway.Service.Services.Identity
{
  public class TokenService: ITokenService
  {
    private readonly IJwtConfig fJwtConfig;

    private SymmetricSecurityKey SigningKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fJwtConfig.BearerTokenSymetricKey));

    public TokenService(IJwtConfig jwtConfig)
    {
      fJwtConfig = jwtConfig;
    }

    public string GenerateUserJwt(ObjectId userId, DateTime? issuedAtUtc = null)
    {
      var utcNow = DateTime.UtcNow;
      string issuedAtString = issuedAtUtc?.ToString() ?? utcNow.ToString();
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, userId.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, UniqueKey.Create()),
        new Claim(JwtRegisteredClaimNames.Iat, issuedAtString, ClaimValueTypes.DateTime)
      };

      var credentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);

      var handler = new JwtSecurityTokenHandler();
      var token = new JwtSecurityToken(
        fJwtConfig.BearerTokenIssuer,
        fJwtConfig.BearerTokenAudience,
        claims,
        utcNow,
        utcNow.AddSeconds(fJwtConfig.BearerTokenDuration),
        credentials);

      return handler.WriteToken(token);
    }

    public TokenDoc GenerateOrRenewRefreshToken(string? oldToken)
    {
      return new TokenDoc
      {
        Value = oldToken ?? SecurityUtils.GetUrlToken(),
        ExpirationUtc = DateTime.UtcNow.AddSeconds(fJwtConfig.RefreshTokenDuration)
      };
    }

    public (ObjectId, DateTime) ValidateJwtWithoutExpiration(string jwtString)
    {
      var validator = new JwtValidator();

      if (!validator.Validate(jwtString, ValidationParameters))
        return (ObjectId.Empty, default);

      if (!ObjectId.TryParse(validator.Principal!.Identity!.Name, out var id))
        return (ObjectId.Empty, default);

      var issuedAtClaim = validator.Principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Iat);

      if (issuedAtClaim is null || !DateTime.TryParse(issuedAtClaim.Value, out var issuedAtUtc))
        return (ObjectId.Empty, default);

      return (id, issuedAtUtc);
    }

    private TokenValidationParameters ValidationParameters =>
      new TokenValidationParameters
      {
        ValidateLifetime = false,
        ValidateAudience = true,
        ValidAudience = fJwtConfig.BearerTokenAudience,
        ValidateIssuer = true,
        ValidIssuer = fJwtConfig.BearerTokenIssuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SigningKey
      };
  }
}
