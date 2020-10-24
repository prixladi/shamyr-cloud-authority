using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Clients;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Services;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Clients
{
  public class PostRequestHandler: IRequestHandler<PostRequest, IdModel>
  {
    private readonly IEmailTemplateRepository fEmailTemplateRepository;
    private readonly IClientRepository fClientRepository;
    private readonly ISecretService fSecretService;

    public PostRequestHandler(
      IEmailTemplateRepository emailTemplateRepository,
      IClientRepository clientRepository,
      ISecretService secretService)
    {
      fEmailTemplateRepository = emailTemplateRepository;
      fClientRepository = clientRepository;
      fSecretService = secretService;
    }

    public async Task<IdModel> Handle(PostRequest request, CancellationToken cancellationToken)
    {
      if (await fClientRepository.ExistsByClientNameAsync(request.Model.Name, cancellationToken))
        throw new ConflictException($"Client with name '{request.Model.Name}' already exists.");

      await CheckEmailTemplateIdsAsync(request, cancellationToken);

      SecretDoc? secret = null;
      if (request.Model.Secret is not null)
        secret = fSecretService.CreateSecret(request.Model.Secret).ToDoc();

      var doc = request.Model.ToDoc(secret);
      await fClientRepository.InsertAsync(doc, cancellationToken);

      return new IdModel { Id = doc.Id };
    }

    public async Task CheckEmailTemplateIdsAsync(PostRequest request, CancellationToken cancellationToken)
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
