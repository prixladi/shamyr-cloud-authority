using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Extensions;
using Shamyr.Cloud.Gateway.Service.Repositories.Users;
using Shamyr.Cloud.Gateway.Service.Requests.CurrentUser;
using Shamyr.Cloud.Gateway.Service.Services.Identity;
using Shamyr.Cloud.Services;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.CurrentUser
{
  public class PatchPasswordRequestHandler: IRequestHandler<PatchPasswordRequest>
  {
    private readonly IIdentityService fIdentityService;
    private readonly IUserRepository fUserRepository;
    private readonly ISecretService fSecretService;

    public PatchPasswordRequestHandler(IIdentityService identityService, IUserRepository userRepository, ISecretService secretService)
    {
      fIdentityService = identityService;
      fUserRepository = userRepository;
      fSecretService = secretService;
    }

    public async Task<Unit> Handle(PatchPasswordRequest request, CancellationToken cancellationToken)
    {
      var user = fIdentityService.Current;

      if (!fSecretService.ComparePasswords(request.Model.OldPassword, user.Secret))
        throw new BadRequestException($"Invalid password provided.");

      var secret = fSecretService.CreateSecret(request.Model.NewPassword);
      await fUserRepository.SetUserSecretAsync(user.UserId, secret.ToDoc(), cancellationToken);

      return Unit.Value;
    }
  }
}
