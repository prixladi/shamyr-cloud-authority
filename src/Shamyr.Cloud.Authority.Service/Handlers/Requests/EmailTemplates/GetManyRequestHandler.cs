using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.EmailTemplates
{
  public class GetManyRequestHandler: IRequestHandler<GetManyRequest, ICollection<PreviewModel>>
  {
    private readonly IEmailTemplateRepository fTemplateRepository;

    public GetManyRequestHandler(IEmailTemplateRepository templateRepository)
    {
      fTemplateRepository = templateRepository;
    }

    public async Task<ICollection<PreviewModel>> Handle(GetManyRequest request, CancellationToken cancellationToken)
    {
      var filter = request.Filter.ToDto();
      var dtos = await fTemplateRepository.GetAsync(filter, cancellationToken);
      return dtos.ToPreview();
    }
  }
}
