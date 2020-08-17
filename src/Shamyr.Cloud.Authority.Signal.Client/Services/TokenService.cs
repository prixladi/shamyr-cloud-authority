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
  public class TokenService: ITokenService, IDisposable
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

      var parameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfiguration.Issuer,
        ValidAudience = tokenConfiguration.Audience,
        IssuerSigningKey = fRsa.ToSecurityKey(tokenConfiguration.PublicKey, true)
      };

      var principal = JwtHandler.ValidateToken(jwt, parameters);
      if (principal == null)
        return null;

      return CreateModel(principal);
    }

    public void Dispose()
    {
      fRsa.Dispose();
    }

    private UserModel CreateModel(ClaimsPrincipal principal)
    {
      string id = principal.FindFirst(ClaimTypes.Name)?.Value ?? throw new InvalidOperationException($"Token is missing '{ClaimTypes.Name}' claim.");
      string username = principal.FindFirst(ClaimTypes.Actor)?.Value ?? throw new InvalidOperationException($"Token is missing '{ClaimTypes.Actor}' claim.");
      string email = principal.FindFirst(ClaimTypes.Email)?.Value ?? throw new InvalidOperationException($"Token is missing '{ClaimTypes.Email}' claim.");
      string grant = principal.FindFirst(ClaimTypes.Email)?.Value ?? throw new InvalidOperationException($"Token is missing '{Constants._GrantClaimType}' claim.");
      bool admin = principal.IsInRole(nameof(UserModel.Admin));

      return new UserModel
      {
        Id = id,
        Email = email,
        Username = username,
        Admin = admin,
        GrantType = grant
      };
    }
  }
}
