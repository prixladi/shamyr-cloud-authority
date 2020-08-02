using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.EmailTemplates
{
  public class PatchNameRequestHandler: IRequestHandler<PatchNameRequest>
  {
    private readonly IEmailTemplateRepository fEmailTemplateRepository;

    public PatchNameRequestHandler(IEmailTemplateRepository emailTemplateRepository)
    {
      fEmailTemplateRepository = emailTemplateRepository;
    }

    public async Task<Unit> Handle(PatchNameRequest request, CancellationToken cancellationToken)
    {
      if (!await fEmailTemplateRepository.UpdatePropAsync(request.TemplateId, doc => doc.Name, request.Name, cancellationToken))
        throw new NotFoundException($"Email template with ID {request.TemplateId} does not exist.");

      return Unit.Value;
    }
  }
}
