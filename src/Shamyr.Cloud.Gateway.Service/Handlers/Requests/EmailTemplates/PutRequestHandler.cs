using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Extensions;
using Shamyr.Cloud.Gateway.Service.Repositories;
using Shamyr.Cloud.Gateway.Service.Requests.EmailTemplates;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.EmailTemplates
{
  public class PutRequestHandler: IRequestHandler<PutRequest>
  {
    private readonly IEmailTemplateRepository fTemplateRepository;

    public PutRequestHandler(IEmailTemplateRepository templateRepository)
    {
      fTemplateRepository = templateRepository;
    }

    public async Task<Unit> Handle(PutRequest request, CancellationToken cancellationToken)
    {
      var template = await fTemplateRepository.GetAsync(request.TemplateId, cancellationToken);
      if (template == null)
        throw new NotFoundException($"Email template with ID '{request.TemplateId}' does not exist.");

      if (template.Type != request.Model.Type && await fTemplateRepository.ExistsByTypeAsync(request.Model.Type, cancellationToken))
        throw new ConflictException($"Email template with type '{request.Model.Type}' already exists.");

      var updateDto = request.Model.ToDto();
      await fTemplateRepository.UpdateAsync(request.TemplateId, updateDto, cancellationToken);
      return Unit.Value;
    }
  }
}
