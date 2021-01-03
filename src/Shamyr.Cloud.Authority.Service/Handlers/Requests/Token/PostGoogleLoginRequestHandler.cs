using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Token;
using Shamyr.Cloud.Authority.Service.Services.Identity;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Token
{
  public class PostGoogleLoginRequestHandler: PostLoginRequestHandlerBase<PostGoogleLoginRequest>
  {
    private readonly IUserRepository fUserRepository;

    protected override string GrantType => "google";

    public PostGoogleLoginRequestHandler(
      ITokenService tokenService,
      IUserTokenRepository userTokenRepository,
      IUserRepository userRepository)
      : base(tokenService, userTokenRepository)
    {
      fUserRepository = userRepository;
    }

    protected override async Task<UserDoc> GetUserAsync(PostGoogleLoginRequest request, CancellationToken cancellationToken)
    {
      try
      {
        var payload = await GoogleJsonWebSignature.ValidateAsync(request.Model.IdToken);
        // By logging with google we automaticaly validate user's email, hence verification
        var user = await fUserRepository.GetByEmailAndVerifyAsync(payload.Email, cancellationToken);
        if (user is null)
        {
          user = payload.ToUserDoc();
          await fUserRepository.InsertAsync(user, cancellationToken);
        }

        return user;
      }
      catch (InvalidJwtException)
      {
        throw new BadRequestException("Google token is invalid.");
      }
    }
  }
}
