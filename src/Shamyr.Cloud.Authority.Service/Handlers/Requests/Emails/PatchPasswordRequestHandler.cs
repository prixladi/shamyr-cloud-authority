using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.AspNetCore.Services;
using Shamyr.Cloud.Authority.Service.Emails;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Emails;
using Shamyr.Cloud.Authority.Service.Services;
using Shamyr.Security;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Emails
{
  public class PatchPasswordRequestHandler: IRequestHandler<PatchPasswordResetRequest>
  {
    private readonly IUserRepository fUserRepository;
    private readonly IEmailService fEmailService;
    private readonly ITelemetryService fTelemetryService;

    public PatchPasswordRequestHandler(
      IUserRepository userRepository,
      IEmailService emailService,
      ITelemetryService telemetryService)
    {
      fUserRepository = userRepository;
      fEmailService = emailService;
      fTelemetryService = telemetryService;
    }

    public async Task<Unit> Handle(PatchPasswordResetRequest request, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetByEmailAsync(request.Email, cancellationToken);
      if (user is null)
        throw new NotFoundException($"Account with email '{request.Email}' does not exist.");

      if (user.PasswordToken is null)
        user = await fUserRepository.SetPasswordTokenAsync(request.Email, SecurityUtils.GetUrlToken(), cancellationToken);

      Debug.Assert(user != null);

      var context = fTelemetryService.GetRequestContext();
      // TODO: Solve "too many password reset requests"
      await fEmailService.SendEmailAsync(PasswordResetEmailContext.New(user, context), cancellationToken);
      return Unit.Value;
    }
  }
}
