using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Authorization;
using Shamyr.Cloud.Gateway.Service.Extensions.Models;
using Shamyr.Cloud.Gateway.Service.Repositories.Users;
using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service.Authentication.JwtBearer
{
  public class JwtBearerAuthenticationHandler: JwtBearerHandler
  {
    private readonly IUserRepository fUserRepository;

    public JwtBearerAuthenticationHandler(
      IUserRepository userRepository,
      IOptionsMonitor<JwtBearerOptions> options,
      ILoggerFactory logger,
      UrlEncoder encoder,
      ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
      fUserRepository = userRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      var result = await base.HandleAuthenticateAsync();

      if (!result.Succeeded)
        return result;

      var principal = result.Principal!;

      var (user, userIdentity) = await ValidateUserAsync(principal.Identity!.Name, (ClaimsIdentity)principal.Identity, Context.RequestAborted);
      ValidateLogin(user, principal);

      foreach (var role in UserPolicy.GetRoles((PermissionKind?)user.UserPermission?.Kind))
        userIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

      var userPrincipal = new ClaimsPrincipal(userIdentity);
      var ticket = new AuthenticationTicket(userPrincipal, Options.Challenge);

      return AuthenticateResult.Success(ticket);
    }

    private async Task<(UserDoc, UserIdentityProfile)> ValidateUserAsync(string? name, ClaimsIdentity identity, CancellationToken cancellationToken)
    {
      if (name is null || !ObjectId.TryParse(name, out var userId))
        throw new Exception($"User ID has invalid format.");

      var user = await fUserRepository.GetAsync(userId, cancellationToken);

      if (user is null)
        throw new UnauthorizedException($"User with ID '{userId}' doesn't exist.");

      var userIdentity = user.ToIdentity(identity);

      if (!string.IsNullOrEmpty(user.EmailToken))
        throw new EmailNotVerifiedException(userIdentity.Base);

      if (user.Disabled)
        throw new UserDisabledException();

      return (user, userIdentity);
    }

    public static void ValidateLogin(UserDoc user, ClaimsPrincipal principal)
    {
      var issuedAtClaim = principal.FindFirst(JwtRegisteredClaimNames.Iat);
      if (issuedAtClaim is null)
        throw new Exception($"User 'issued-at' claim is null.");

      if (!DateTime.TryParse(issuedAtClaim.Value, out var issuedAtUtc))
        throw new Exception($"User 'issued-at' claim has invalid format.");

      if (user.LogoutUtc.HasValue && issuedAtUtc < user.LogoutUtc.Value)
        throw new UnauthorizedException($"User is logged out.");
    }
  }
}
