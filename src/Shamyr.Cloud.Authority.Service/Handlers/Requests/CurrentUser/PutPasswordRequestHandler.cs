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
  public class PutPasswordRequestHandler: IRequestHandler<PutPasswordRequest>
  {
    private readonly IIdentityService fIdentityService;
    private readonly IUserRepository fUserRepository;
    private readonly ISecretService fSecretService;

    public PutPasswordRequestHandler(IIdentityService identityService, IUserRepository userRepository, ISecretService secretService)
    {
      fIdentityService = identityService;
      fUserRepository = userRepository;
      fSecretService = secretService;
    }

    public async Task<Unit> Handle(PutPasswordRequest request, CancellationToken cancellationToken)
    {
      var user = fIdentityService.Current;
      if (user.Secret == null)
        throw new ConflictException("User does not have any password set.");

      if (!fSecretService.ComparePasswords(request.Model.OldPassword, user.Secret))
        throw new BadRequestException($"Invalid password provided.");

      var secret = fSecretService.CreateSecret(request.Model.NewPassword);
      await fUserRepository.SetSecretAsync(user.UserId, secret.ToDoc(), cancellationToken);

      return Unit.Value;
    }
  }
}
