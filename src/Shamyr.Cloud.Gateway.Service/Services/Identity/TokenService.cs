using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Configs;
using Shamyr.Cloud.Gateway.Service.Dtos;
using Shamyr.Security;
using Shamyr.Security.IdentityModel;

namespace Shamyr.Cloud.Gateway.Service.Services.Identity
{
  public class TokenService: ITokenService
  {
    private readonly IJwtConfig fConfig;

    public TokenService(IJwtConfig config)
    {
      fConfig = config;
    }

    public string GenerateUserJwt(UserDoc user, DateTime? issuedAtUtc = null)
    {
      var realIssuedAt = issuedAtUtc ?? DateTime.UtcNow;
      var claims = new Claim[]
      {
        new Claim(ClaimTypes.Name, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(nameof(UserDoc.Admin).ToLower(), user.Admin.ToString(), ClaimValueTypes.Boolean)
      };

      using var rsa = RSA.Create();
      var dto = new JwtSecurityTokenDto
      {
        Audience = fConfig.BearerTokenAudience,
        Issuer = fConfig.BearerTokenIssuer,
        Claims = claims,
        NotBefore = realIssuedAt,
        Expires = realIssuedAt.AddSeconds(fConfig.BearerTokenDuration),
        SigningCredentials = rsa.ToSigningCredentials(fConfig.BearerPrivateKey)
      };

      return JwtHandler.CreateToken(dto);
    }

    public TokenDoc GenerateOrRenewRefreshToken(string? oldToken)
    {
      return new TokenDoc
      {
        Value = oldToken ?? SecurityUtils.GetUrlToken(),
        ExpirationUtc = DateTime.UtcNow.AddSeconds(fConfig.RefreshTokenDuration)
      };
    }

    public ValidatedJwtDto? ValidateJwtWithoutExpiration(string token)
    {
      using var rsa = RSA.Create();
      var parameters = new TokenValidationParameters
      {
        ValidateLifetime = false,
        ValidateAudience = true,
        ValidAudience = fConfig.BearerTokenAudience,
        ValidateIssuer = true,
        ValidIssuer = fConfig.BearerTokenIssuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = rsa.ToSecurityKey(fConfig.BearerPublicKey, true)
      };

      var principal = JwtHandler.ValidateToken(token, parameters);
      if (principal == null)
        return null;

      if (!ObjectId.TryParse(principal.Identity!.Name, out var id))
        return null;

      var nameClaim = principal.FindFirst(JwtRegisteredClaimNames.Iat);
      if (nameClaim == null || !long.TryParse(nameClaim.Value, out var iatSeconds))
        return null;

      var offset = DateTimeOffset.FromUnixTimeSeconds(iatSeconds);

      return new ValidatedJwtDto
      {
        UserId = id,
        IssuedAtUtc = offset.UtcDateTime
      };
    }
  }
}
