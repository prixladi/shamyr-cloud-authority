using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models.Logins;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Logins;
using Shamyr.Cloud.Authority.Service.Services.Identity;
using Shamyr.Cloud.Services;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Logins
{
  public class PostRequestHandler: IRequestHandler<PostRequest, TokensModel>
  {
    private readonly IUserRepository fUserRepository;
    private readonly ISecretService fSecretService;
    private readonly ITokenService fTokenService;
    private readonly IUserTokenRepository fUserTokenRepository;

    public PostRequestHandler(
      IUserRepository userRepository,
      ISecretService secretService,
      ITokenService tokenService,
      IUserTokenRepository userTokenRepository)
    {
      fUserRepository = userRepository;
      fSecretService = secretService;
      fTokenService = tokenService;
      fUserTokenRepository = userTokenRepository;
    }

    public async Task<TokensModel> Handle(PostRequest request, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetByUsernameAsync(request.Model.Username, cancellationToken);
      if (user is null)
        throw new BadRequestException($"Invalid username '{request.Model.Username}'.");

      if (!fSecretService.ComparePasswords(request.Model.Password, user.Secret.ToModel()))
        throw new BadRequestException($"Invalid password.");

      if (user.Disabled)
        throw new UserDisabledException();

      string jwt = fTokenService.GenerateUserJwt(user);

      var userToken = fTokenService.GenerateOrRenewRefreshToken(user.RefreshToken?.Value);
      await fUserTokenRepository.SetRefreshTokenAsync(user.Id, userToken, cancellationToken);

      return new TokensModel
      {
        BearerToken = jwt,
        RefreshToken = userToken.Value,
        RefreshTokenExpirationUtc = userToken.ExpirationUtc,
      };
    }
  }
}
