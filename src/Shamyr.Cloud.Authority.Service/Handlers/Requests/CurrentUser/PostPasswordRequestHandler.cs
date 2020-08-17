using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.CurrentUser;
using Shamyr.Cloud.Authority.Service.Services.Identity;
using Shamyr.Cloud.Services;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.CurrentUser
{
  public class PostPasswordRequestHandler: IRequestHandler<PostPasswordRequest>
  {
    private readonly IIdentityService fIdentityService;
    private readonly ISecretService fSecretService;
    private readonly IUserRepository fUserRepository;

    public PostPasswordRequestHandler(IIdentityService identityService, ISecretService secretService, IUserRepository userRepository)
    {
      fIdentityService = identityService;
      fSecretService = secretService;
      fUserRepository = userRepository;
    }

    public async Task<Unit> Handle(PostPasswordRequest request, CancellationToken cancellationToken)
    {
      var user = fIdentityService.Current;
      if (user.Secret != null)
        throw new ConflictException("User already has his password set.");

      var secret = fSecretService.CreateSecret(request.Model.Password);

      if (!await fUserRepository.TryAddSecretAsync(user.UserId, secret.ToDoc(), cancellationToken))
        throw new ConflictException("User already has his password set.");

      return Unit.Value;
    }
  }
}
