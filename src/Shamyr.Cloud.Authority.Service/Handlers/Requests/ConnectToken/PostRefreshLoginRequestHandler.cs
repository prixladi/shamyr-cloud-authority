using System;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.ConnectToken;
using Shamyr.Cloud.Authority.Service.Services.Identity;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.ConnectToken
{
  public class PostRefreshLoginRequestHandler: PostLoginRequestHandlerBase<PostRefreshLoginRequest>
  {
    private readonly IUserRepository fUserRepository;

    protected override string GrantType => "refresh";

    public PostRefreshLoginRequestHandler(
      IUserRepository userRepository,
      ITokenService tokenService,
      IUserTokenRepository userTokenRepository)
      : base(tokenService, userTokenRepository)
    {
      fUserRepository = userRepository;
    }

    protected override async Task<UserDoc> GetUserAsync(PostRefreshLoginRequest request, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetByRefreshTokenAsync(request.Model.RefreshToken, cancellationToken);
      if (user is null)
        throw new BadRequestException("User with given refresh token  was not found.");
      if (user.RefreshToken!.ExpirationUtc < DateTime.UtcNow)
        throw new BadRequestException("Refresh token is expired.");

      return user;
    }
  }
}
