using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using Shamyr.Cloud.Identity.Service.Configs;
using Shamyr.Cloud.Identity.Service.Dtos;
using Shamyr.Cloud.Identity.Service.Extensions;
using Shamyr.Cloud.Identity.Service.Repositories;
using Shamyr.Security.Tokens.Jwt;

namespace Shamyr.Cloud.Identity.Service.Services
{
  public class TokenService: ITokenService
  {
    private readonly IJwtConfig fJwtConfig;
    private readonly IUserRepository fUserRepository;

    public TokenService(IJwtConfig jwtConfig, IUserRepository userRepository)
    {
      fJwtConfig = jwtConfig;
      fUserRepository = userRepository;
    }

    public async Task<(IdentityResult, UserIdentityProfileDto?)> ValidateTokenAsync(string token, CancellationToken cancellationToken)
    {
      if (!IsJwt(token))
        throw new BadRequestException($"Specified token ({token}) is not jwt.");

      if (!TryValidateJwt(token, out var userId, out var issuedAtUtc))
        return (IdentityResult.Invalid, null);

      var user = await fUserRepository.GetAsync(userId, cancellationToken);
      if (user is null)
        return (IdentityResult.NotFound, null);

      if (user.LogoutUtc.HasValue && issuedAtUtc < user.LogoutUtc.Value)
        return (IdentityResult.LoggedOut, null);

      if (user.EmailToken != null)
        return (IdentityResult.NotVerified, user.ToDto());

      if (user.Disabled)
        return (IdentityResult.Disabled, user.ToDto());

      return (IdentityResult.Ok, user.ToDto());
    }

    private bool TryValidateJwt(string token, out ObjectId userId, out DateTime issuedAtUtc)
    {
      userId = default;
      issuedAtUtc = default;

      var validator = new JwtValidator();

      if (!validator.Validate(token, ValidationParameters))
        return false;

      Debug.Assert(validator.Principal != null && validator.Principal.Identity != null);

      if (!ObjectId.TryParse(validator.Principal.Identity.Name, out userId))
        return false;

      var issuedAtClaim = validator.Principal.FindFirst(JwtRegisteredClaimNames.Iat);

      if (issuedAtClaim is null || !DateTime.TryParse(issuedAtClaim.Value, out issuedAtUtc))
        return false;

      return true;
    }

    private static bool IsJwt(string jwtString)
    {
      var regex = new Regex(@"^[A-Za-z0-9-_=]+\.[A-Za-z0-9-_=]+\.?[A-Za-z0-9-_.+/=]*$");
      return regex.IsMatch(jwtString);
    }

    private TokenValidationParameters ValidationParameters => new TokenValidationParameters
    {
      ValidIssuer = fJwtConfig.BearerTokenIssuer,
      ValidAudience = fJwtConfig.BearerTokenAudience,
      ValidateLifetime = true,
      ClockSkew = TimeSpan.FromMinutes(5),
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fJwtConfig.BearerTokenSymetricKey)),
      NameClaimType = ClaimTypes.Name,
      RoleClaimType = ClaimTypes.Role
    };
  }
}
