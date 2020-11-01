using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Shamyr.Cloud.Authority.Client.Repositories;
using Shamyr.Cloud.Authority.Models;
using Shamyr.Extensions.DependencyInjection;
using Shamyr.Security.IdentityModel;

namespace Shamyr.Cloud.Authority.Client.Services
{
  [Singleton]
  internal class TokenService: ITokenService
  {
    private readonly ITokenConfigurationRepository fTokenConfigurationRepository;
    private readonly RSA fRsa;

    public TokenService(ITokenConfigurationRepository tokenConfigurationRepository)
    {
      fTokenConfigurationRepository = tokenConfigurationRepository;
      fRsa = RSA.Create();
    }

    public async Task<UserModel?> TryGetUserAsync(string jwt, CancellationToken cancellationToken)
    {
      var tokenConfiguration = await fTokenConfigurationRepository.GetAsync(cancellationToken);
      var parameters = GetValidationParams(tokenConfiguration);

      var principal = JwtHandler.ValidateToken(jwt, parameters);
      if (principal is null)
        return null;

      return CreateModel(principal);
    }

    private TokenValidationParameters GetValidationParams(TokenConfigurationModel config)
    {
      return new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config.Issuer,
        ValidAudience = config.Audience,
        IssuerSigningKey = fRsa.ToSecurityKey(config.PublicKey, true)
      };
    }

    private UserModel CreateModel(ClaimsPrincipal principal)
    {
      return new UserModel
      {
        Id = GetClaim(principal, ClaimTypes.Name),
        Email = GetClaim(principal, ClaimTypes.Actor),
        Username = GetClaim(principal, ClaimTypes.Email),
        GivenName = GetClaim(principal, ClaimTypes.GivenName),
        FamilyName = GetClaim(principal, ClaimTypes.Surname),
        Admin = principal.IsInRole(nameof(UserModel.Admin)),
      };
    }

    private string GetClaim(ClaimsPrincipal principal, string claimName)
    {
      var claim = principal.FindFirst(claimName);
      if (claim is null)
        throw new InvalidOperationException($"Token is missing '{ClaimTypes.Name}' claim.");

      return claim.Value;
    }
  }
}
