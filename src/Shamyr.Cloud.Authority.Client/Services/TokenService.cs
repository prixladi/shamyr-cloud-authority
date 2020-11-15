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
        IssuerSigningKey = fRsa.ToSecurityKey(config.PublicKey, true),
        NameClaimType = Constants._NameClaim,
        RoleClaimType = Constants._RoleClaim
      };
    }

    private UserModel CreateModel(ClaimsPrincipal principal)
    {
      return new UserModel
      {
        Id = GetClaim(principal, Constants._NameClaim),
        Email = GetClaim(principal, Constants._UsernameClaim),
        Username = TryGetClaim(principal, Constants._EmailClaim),
        GivenName = TryGetClaim(principal, Constants._GivenNameClaim),
        FamilyName = TryGetClaim(principal, Constants._FamilyNameClaim),
        Admin = principal.IsInRole(nameof(UserModel.Admin)),
      };
    }

    private string? TryGetClaim(ClaimsPrincipal principal, string claimName)
    {
      var claim = principal.FindFirst(claimName);
      return claim?.Value;
    }

    private string GetClaim(ClaimsPrincipal principal, string claimName)
    {
      return TryGetClaim(principal, claimName) ?? throw new InvalidOperationException($"Token is missing '{claimName}' claim.");
    }
  }
}
