using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shamyr.AspNetCore.Services;
using Shamyr.Cloud.Identity.Client.Factories;
using Shamyr.Cloud.Identity.Client.Models;

namespace Shamyr.Cloud.Identity.Client.Authentication
{
  public class IdentityAuthenticationHandler: AuthenticationHandler<IdentityAuthenticationOptions>
  {
    private readonly IIdentityClient fIdentityClient;
    private readonly ITelemetryService fTelemetryService;
    private readonly IPrincipalFactory fPrincipalFactory;
    private readonly IServiceProvider fServiceProvider;

    public IdentityAuthenticationHandler(
      IOptionsMonitor<IdentityAuthenticationOptions> options,
      ILoggerFactory logger,
      UrlEncoder encoder,
      ISystemClock clock,
      IServiceProvider serviceProvider)
        : base(options, logger, encoder, clock)
    {
      fIdentityClient = serviceProvider.GetRequiredService<IIdentityClient>();
      fTelemetryService = serviceProvider.GetRequiredService<ITelemetryService>();
      fPrincipalFactory = serviceProvider.GetRequiredService<IPrincipalFactory>();

      fServiceProvider = serviceProvider;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      var token = GetIdentityToken();
      if (string.IsNullOrEmpty(token) || !IsJwt(token))
        return AuthenticateResult.Fail("Wrong format or missing token.");

      var context = fTelemetryService.GetRequestContext();

      var validationModel = await fIdentityClient.GetUserIdentityByJwtAsync(context, token, Context.RequestAborted);

      if (validationModel is null || validationModel.Result == IdentityResult.Invalid)
        return AuthenticateResult.Fail("Invalid token.");
      if (validationModel.Result == IdentityResult.NotFound)
        return AuthenticateResult.Fail("User not found.");
      if (validationModel.Result == IdentityResult.LoggedOut)
        return AuthenticateResult.Fail("User logged out.");
      if (validationModel.Result == IdentityResult.Disabled)
        throw new UserDisabledException();

      Debug.Assert(validationModel.User != null);

      if (validationModel.Result == IdentityResult.NotVerified)
        throw new EmailNotVerifiedException(validationModel.User.ToIdentity());

      var userPrincipal = await fPrincipalFactory.CreateAsync(fServiceProvider, Scheme.Name, validationModel.User, Context.RequestAborted);
      var ticket = new AuthenticationTicket(userPrincipal, Scheme.Name);
      return AuthenticateResult.Success(ticket);
    }

    private string? GetIdentityToken()
    {
      string? token = Request
        .Query
        .SingleOrDefault(x => string.Equals(x.Key, "Identity", StringComparison.OrdinalIgnoreCase))
        .Value
        .FirstOrDefault();

      if (string.IsNullOrEmpty(token))
      {
        string? accessToken = Request
        .Headers["Authorization"]
        .FirstOrDefault(x => x.StartsWith("Identity ", StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(accessToken))
          token = accessToken.Substring("Identity ".Length);
      }

      return token;
    }

    private static bool IsJwt(string jwtString)
    {
      var regex = new Regex(@"^[A-Za-z0-9-_=]+\.[A-Za-z0-9-_=]+\.?[A-Za-z0-9-_.+/=]*$");
      return regex.IsMatch(jwtString);
    }
  }
}
