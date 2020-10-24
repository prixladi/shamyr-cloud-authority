using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Models;
using Shamyr.Cloud.Authority.Service.Configs;
using Shamyr.Cloud.Authority.Service.Requests.Token;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Token
{
  public class GetConfigurationRequestHandler: IRequestHandler<GetConfigurationRequest, TokenConfigurationModel>
  {
    private readonly IJwtConfig fConfig;

    public GetConfigurationRequestHandler(IJwtConfig config)
    {
      fConfig = config;
    }

    public Task<TokenConfigurationModel> Handle(GetConfigurationRequest request, CancellationToken cancellationToken)
    {
      var configuration = new TokenConfigurationModel
      {
        PublicKey = fConfig.BearerPublicKey,
        KeyDuration = fConfig.BearerTokenDuration,
        SignatureAlgorithm = "RS256",
        Issuer = fConfig.BearerTokenIssuer,
        Audience = fConfig.BearerTokenAudience
      };

      return Task.FromResult(configuration);
    }
  }
}
