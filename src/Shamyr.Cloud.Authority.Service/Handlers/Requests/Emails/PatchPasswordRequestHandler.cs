using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.AspNetCore.ApplicationInsights.Services;
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
    private readonly IClientRepository fClientRepository;
    private readonly ITelemetryService fTelemetryService;

    public PatchPasswordRequestHandler(
      IUserRepository userRepository,
      IEmailService emailService,
      IClientRepository clientRepository,
      ITelemetryService telemetryService)
    {
      fUserRepository = userRepository;
      fEmailService = emailService;
      fClientRepository = clientRepository;
      fTelemetryService = telemetryService;
    }

    public async Task<Unit> Handle(PatchPasswordResetRequest request, CancellationToken cancellationToken)
    {
      var client = await fClientRepository.GetAsync(request.Model.ClientId, cancellationToken);
      if (client is null)
        throw new BadRequestException($"Client with ID '{request.Model.ClientId}' doesn't exist.");

      var user = await fUserRepository.GetByEmailAsync(request.Email, cancellationToken);
      if (user is null) // hide the fact that user with this email does not exists
        return Unit.Value;

      if (user.PasswordToken is null)
        user = await fUserRepository.SetPasswordTokenAsync(request.Email, SecurityUtils.GetUrlToken(), cancellationToken);

      Debug.Assert(user is not null);

      var context = fTelemetryService.GetRequestContext();
      // TODO: Solve "too many password reset requests"
      await fEmailService.SendEmailAsync(PasswordResetEmailContext.New(user, client, context), cancellationToken);
      return Unit.Value;
    }
  }
}
