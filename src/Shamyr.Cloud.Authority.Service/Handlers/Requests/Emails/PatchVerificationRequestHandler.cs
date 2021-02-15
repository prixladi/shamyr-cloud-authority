using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Emails;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Emails;
using Shamyr.Cloud.Authority.Service.Services;
using Shamyr.Cloud.Database.Documents;
using Shamyr.ExtensibleLogging;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Emails
{
  public class PatchVerificationRequestHandler: IRequestHandler<PatchVerificationRequest>
  {
    private readonly IUserRepository fUserRepository;
    private readonly IEmailService fEmailService;
    private readonly IClientRepository fClientRepository;
    private readonly ILoggingContextService fLoggingContextService;

    public PatchVerificationRequestHandler(
      IUserRepository userRepository,
      IEmailService emailService,
      IClientRepository clientRepository,
      ILoggingContextService loggingContextService)
    {
      fUserRepository = userRepository;
      fEmailService = emailService;
      fClientRepository = clientRepository;
      fLoggingContextService = loggingContextService;
    }

    public async Task<Unit> Handle(PatchVerificationRequest request, CancellationToken cancellationToken)
    {
      var client = await fClientRepository.GetAsync(request.Model.ClientId, cancellationToken);
      if (client is null)
        throw new BadRequestException($"Client with ID '{request.Model.ClientId}' doesn't exist.");

      var user = await GetUserByEmailOrThrowAsync(request.Email, cancellationToken);
      if (user.EmailToken is null)
        throw new ConflictException($"Account with email '{request.Email}' is already verified.");

      var context = fLoggingContextService.GetRequestContext();
      await fEmailService.SendEmailAsync(VerifyAccountEmailContext.New(user, client, context), cancellationToken);

      return Unit.Value;
    }

    private async Task<UserDoc> GetUserByEmailOrThrowAsync(string email, CancellationToken cancellationToken)
    {
      var user = await fUserRepository.GetByEmailAsync(email, cancellationToken);
      if (user is null)
        throw new NotFoundException($"Account with email '{email}' does not exist.");

      return user;
    }
  }
}
