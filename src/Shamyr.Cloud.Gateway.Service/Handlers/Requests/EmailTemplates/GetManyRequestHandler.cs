using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Extensions;
using Shamyr.Cloud.Gateway.Service.Models.EmailTemplates;
using Shamyr.Cloud.Gateway.Service.Repositories;
using Shamyr.Cloud.Gateway.Service.Requests.EmailTemplates;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.EmailTemplates
{
  public class GetManyRequestHandler: IRequestHandler<GetManyRequest, ICollection<EmailTemplatePreviewModel>>
  {
    private readonly IEmailTemplateRepository fTemplateRepository;

    public GetManyRequestHandler(IEmailTemplateRepository templateRepository)
    {
      fTemplateRepository = templateRepository;
    }

    public async Task<ICollection<EmailTemplatePreviewModel>> Handle(GetManyRequest request, CancellationToken cancellationToken)
    {
      var dtos = await fTemplateRepository.GetAsync(cancellationToken);
      return dtos.ToPreview();
    }
  }
}
