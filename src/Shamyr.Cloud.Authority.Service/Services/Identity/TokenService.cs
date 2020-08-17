using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using Shamyr.Cloud.Authority.Models;
using Shamyr.Cloud.Authority.Service.Configs;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Security;
using Shamyr.Security.IdentityModel;

namespace Shamyr.Cloud.Authority.Service.Services.Identity
{
  public class TokenService: ITokenService
  {
    private readonly IJwtConfig fConfig;

    public TokenService(IJwtConfig config)
    {
      fConfig = config;
    }

    public string GenerateUserJwt(UserDoc user, string grant)
    {
      var issuedAtUtc = DateTime.UtcNow;
      var claims = new List<Claim>(4)
      {
        new Claim(ClaimTypes.Name, user.Id.ToString()),
        new Claim(ClaimTypes.Actor, user.Username),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(Constants._GrantClaimType, grant)
      };

      if (user.Admin)
        claims.Add(new Claim(ClaimTypes.Role, nameof(UserDoc.Admin)));

      using var rsa = RSA.Create();
      var dto = new JwtSecurityTokenDto
      {
        Audience = fConfig.BearerTokenAudience,
        Issuer = fConfig.BearerTokenIssuer,
        Claims = claims,
        NotBefore = issuedAtUtc,
        Expires = issuedAtUtc.AddSeconds(fConfig.BearerTokenDuration),
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
  }
}
