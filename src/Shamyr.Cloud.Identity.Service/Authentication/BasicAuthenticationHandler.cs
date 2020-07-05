using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Shamyr.AspNetCore.Authentication.Handlers;
using Shamyr.Cloud.Identity.Service.Repositories;
using Shamyr.Cloud.Services;
using Shamyr.Security;

namespace Shamyr.Cloud.Identity.Service.Authentication
{
  public class BasicAuthenticationHandler: BasicAuthenticationHandlerBase<AuthenticationSchemeOptions>
  {
    private readonly IClientRepository fClientRepository;
    private readonly ISecretService fSecretService;

    public BasicAuthenticationHandler(
      IOptionsMonitor<AuthenticationSchemeOptions> options,
      ILoggerFactory loggerFactory,
      UrlEncoder encoder,
      ISystemClock clock,
      IClientRepository clientRepository,
      ISecretService secretService)
      : base(options, loggerFactory, encoder, clock)
    {
      fClientRepository = clientRepository;
      fSecretService = secretService;
    }

    protected override async Task<AuthenticateResult> DoHandleAuthenticateAsync(string clientId, string secretString)
    {
      if (!ObjectId.TryParse(clientId, out var id))
        return AuthenticateResult.Fail("Client id has invalid format");

      var client = await fClientRepository.GetAsync(id, Context.RequestAborted);
      if (client is null)
        return AuthenticateResult.Fail("Invalid client id.");

      var secret = new Secret
      (
        hash: client.Secret.Hash,
        salt: client.Secret.Salt
      );

      if (!fSecretService.ComparePasswords(secretString, secret))
        return AuthenticateResult.Fail("Invalid client secret.");

      var identity = new ClaimsIdentity(Scheme.Name);
      var principal = new ClaimsPrincipal(identity);
      var ticket = new AuthenticationTicket(principal, Scheme.Name);
      return AuthenticateResult.Success(ticket);
    }
  }
}
