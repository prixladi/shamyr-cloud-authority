using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Clients;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Services;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Clients
{
  public class PutRequestHandler: IRequestHandler<PutRequest>
  {
    private readonly IEmailTemplateRepository fEmailTemplateRepository;
    private readonly IClientRepository fClientRepository;
    private readonly ISecretService fSecretService;

    public PutRequestHandler(
      IEmailTemplateRepository emailTemplateRepository,
      IClientRepository clientRepository,
      ISecretService secretService)
    {
      fEmailTemplateRepository = emailTemplateRepository;
      fClientRepository = clientRepository;
      fSecretService = secretService;
    }

    public async Task<Unit> Handle(PutRequest request, CancellationToken cancellationToken)
    {
      var client = await fClientRepository.GetAsync(request.ClientId, cancellationToken);
      if (client is null)
        throw new NotFoundException($"Client with ID '{request.ClientId}' does not exist.");
      if (client.Name != request.Model.Name && await fClientRepository.ExistsByClientNameAsync(request.Model.Name, cancellationToken))
        throw new ConflictException($"Client with clientId '{request.Model.Name}' already exists.");

      await CheckEmailTemplateIdsAsync(request, cancellationToken);

      SecretDoc? secret = null;
      if (request.Model.Secret is not null)
        secret = fSecretService.CreateSecret(request.Model.Secret).ToDoc();

      var updateDto = request.Model.ToDto(secret);
      await fClientRepository.UpdateAsync(request.ClientId, updateDto, cancellationToken);

      return Unit.Value;
    }

    public async Task CheckEmailTemplateIdsAsync(PutRequest request, CancellationToken cancellationToken)
    {
      var passwordResetId = request.Model.PasswordResetEmailTemplateId;
      if (passwordResetId.HasValue && !await fEmailTemplateRepository.ExistsAsync(passwordResetId.Value, cancellationToken))
        throw new BadRequestException($"Email template with ID '{passwordResetId.Value}' dosn't exist.");

      var verifyAccountId = request.Model.PasswordResetEmailTemplateId;
      if (verifyAccountId.HasValue && !await fEmailTemplateRepository.ExistsAsync(verifyAccountId.Value, cancellationToken))
        throw new BadRequestException($"Email template with ID '{verifyAccountId.Value}' dosn't exist.");
    }
  }
}
