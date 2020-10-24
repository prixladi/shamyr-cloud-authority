using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Models.Token;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Services.Identity;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Token
{
  public abstract class PostLoginRequestHandlerBase<TRequest>: IRequestHandler<TRequest, TokensModel>
    where TRequest : IRequest<TokensModel>
  {
    private readonly ITokenService fTokenService;
    private readonly IUserTokenRepository fUserTokenRepository;

    protected abstract string GrantType { get; }

    protected PostLoginRequestHandlerBase(ITokenService tokenService, IUserTokenRepository userTokenRepository)
    {
      fTokenService = tokenService;
      fUserTokenRepository = userTokenRepository;
    }

    public async Task<TokensModel> Handle(TRequest request, CancellationToken cancellationToken)
    {
      UserDoc user = await GetUserAsync(request, cancellationToken);

      if (user.EmailToken is not null)
        throw new EmailNotVerifiedException(new UserIdentity(user.Id.ToString(), user.Username, user.Email));
      if (user.Disabled)
        throw new UserDisabledException();

      string jwt = fTokenService.GenerateUserJwt(user, GrantType);
      var refreshToken = fTokenService.GenerateOrRenewRefreshToken(user.RefreshToken?.Value);
      await fUserTokenRepository.SetRefreshTokenAsync(user.Id, refreshToken, cancellationToken);

      return new TokensModel
      {
        BearerToken = jwt,
        RefreshToken = refreshToken.Value,
        RefreshTokenExpirationUtc = refreshToken.ExpirationUtc,
      };
    }

    protected abstract Task<UserDoc> GetUserAsync(TRequest request, CancellationToken cancellationToken);
  }
}
