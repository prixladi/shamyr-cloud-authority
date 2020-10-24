using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.EmailTemplates
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
      var updateDto = request.Model.ToDto();
      if (!await fTemplateRepository.UpdateAsync(request.TemplateId, updateDto, cancellationToken))
        throw new NotFoundException($"Email template with ID '{request.TemplateId}' does not exist.");

      return Unit.Value;
    }
  }
}
