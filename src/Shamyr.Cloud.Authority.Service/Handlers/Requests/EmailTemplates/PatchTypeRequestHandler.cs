using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.EmailTemplates
{
  public class PatchTypeRequestHandler: IRequestHandler<PatchTypeRequest>
  {
    private readonly IEmailTemplateRepository fTemplateRepository;

    public PatchTypeRequestHandler(IEmailTemplateRepository templateRepository)
    {
      fTemplateRepository = templateRepository;
    }

    public async Task<Unit> Handle(PatchTypeRequest request, CancellationToken cancellationToken)
    {
      var template = await fTemplateRepository.GetAsync(request.TemplateId, cancellationToken);
      if (template == null)
        throw new NotFoundException($"Email template with ID '{request.TemplateId}' does not exist.");

      if (template.Type != request.Type && await fTemplateRepository.ExistsByTypeAsync(request.Type, cancellationToken))
        throw new ConflictException($"Email template with type '{request.Type}' already exists.");

      await fTemplateRepository.UpdatePropAsync(request.TemplateId, doc => doc.Type, request.Type, cancellationToken);
      return Unit.Value;
    }
  }
}
