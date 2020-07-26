using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Shamyr.Cloud.Identity.Client.Repositories;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Security.IdentityModel;

namespace Shamyr.Cloud.Identity.Client.Services
{
  public class TokenService: ITokenService
  {
    private readonly ITokenConfigurationRepository fTokenConfigurationRepository;

    public TokenService(ITokenConfigurationRepository tokenConfigurationRepository)
    {
      fTokenConfigurationRepository = tokenConfigurationRepository;
    }

    public async Task<UserModel?> TryGetUserAsync(string jwt, CancellationToken cancellationToken)
    {
      var tokenConfiguration = await fTokenConfigurationRepository.GetAsync(cancellationToken);

      using var rsa = RSA.Create();
      var parameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfiguration.Issuer,
        ValidAudience = tokenConfiguration.Audience,
        IssuerSigningKey = rsa.ToSecurityKey(tokenConfiguration.PublicKey, true)
      };

      var principal = JwtHandler.ValidateToken(jwt, parameters);
      if (principal == null)
        return null;

      return CreateModel(principal);
    }

    private UserModel CreateModel(ClaimsPrincipal principal)
    {
      string id = principal.Identity?.Name ?? throw new InvalidOperationException($"Token is missing '{ClaimTypes.Name}' claim.");
      string username = principal.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value ?? throw new InvalidOperationException($"Token is missing '{JwtRegisteredClaimNames.UniqueName}' claim.");
      string email = principal.FindFirst(JwtRegisteredClaimNames.Email)?.Value ?? throw new InvalidOperationException($"Token is missing '{JwtRegisteredClaimNames.Email}' claim.");
      var adminString = principal.FindFirst(nameof(UserModel.Admin))?.Value;
      var admin = adminString != null && bool.Parse(adminString);

      return new UserModel
      {
        Id = id,
        Email = email,
        Username = username,
        Admin = admin,
        Disabled = false,
        Verified = true
      };
    }
  }
}
