using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Emails;
using Shamyr.Cloud.Gateway.Service.Repositories.Users;
using Shamyr.Cloud.Gateway.Service.Requests.Emails;
using Shamyr.Cloud.Gateway.Service.Services;
using Shamyr.Security;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.Emails
{
  public class PatchPasswordRequestHandler: IRequestHandler<PatchPasswordResetRequest>
  {
    private readonly IUserRepository fUserRepository;
    private readonly IEmailService fEmailService;

    public PatchPasswordRequestHandler(IUserRepository userRepository, IEmailService emailService)
    {
      fUserRepository = userRepository;
      fEmailService = emailService;
    }

    public async Task<Unit> Handle(PatchPasswordResetRequest request, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetByEmailAsync(request.Email, cancellationToken);
      if (user is null)
        throw new NotFoundException($"Account with email '{request.Email}' does not exist.");

      if (user.PasswordToken is null)
        user = await fUserRepository.SetPasswordTokenAsync(request.Email, SecurityUtils.GetUrlToken(), cancellationToken);

      // TODO: Solve "too many password reset requests"
      fEmailService.SendEmailAsync(PasswordResetEmail.New(user!));
      return Unit.Value;
    }
  }
}
