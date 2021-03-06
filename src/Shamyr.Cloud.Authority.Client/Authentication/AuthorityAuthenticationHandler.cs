﻿using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shamyr.Cloud.Authority.Client.Factories;
using Shamyr.Cloud.Authority.Client.Services;
using Shamyr.Cloud.Authority.Models;

namespace Shamyr.Cloud.Authority.Client.Authentication
{
  public class AuthorityAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
  {
    private readonly IPrincipalFactory fPrincipalFactory;
    private readonly IServiceProvider fServiceProvider;
    private readonly ITokenService fTokenService;

    public AuthorityAuthenticationHandler(
      IOptionsMonitor<AuthenticationSchemeOptions> options,
      ILoggerFactory logger,
      UrlEncoder encoder,
      ISystemClock clock,
      IServiceProvider serviceProvider,
      ITokenService tokenService)
        : base(options, logger, encoder, clock)
    {
      fPrincipalFactory = serviceProvider.GetRequiredService<IPrincipalFactory>();

      fServiceProvider = serviceProvider;
      fTokenService = tokenService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      var token = GetAuthorityToken();
      if (string.IsNullOrEmpty(token) || !IsJwt(token))
        return AuthenticateResult.Fail("Wrong format or missing token.");

      var user = await fTokenService.TryGetUserAsync(token, Context.RequestAborted);
      if (user is null)
        return AuthenticateResult.Fail("Token is in wrong format.");

      var userPrincipal = await fPrincipalFactory.CreateAsync(fServiceProvider, Constants._AuthenticationName, user, Context.RequestAborted);
      var ticket = new AuthenticationTicket(userPrincipal, Scheme.Name);
      return AuthenticateResult.Success(ticket);
    }

    private string? GetAuthorityToken()
    {
      string? token = Request
        .Query
        .SingleOrDefault(x => string.Equals(x.Key, "Bearer", StringComparison.OrdinalIgnoreCase))
        .Value
        .FirstOrDefault();

      if (string.IsNullOrEmpty(token))
      {
        string? accessToken = Request
          .Headers["Authorization"]
          .FirstOrDefault(x => x.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(accessToken))
          token = accessToken["Bearer ".Length..];
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
