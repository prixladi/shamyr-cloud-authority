using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.EmailTemplates
{
  public class GetRequestHandler: IRequestHandler<GetRequest, EmailTemplateDetailModel>
  {
    private readonly IEmailTemplateRepository fTemplateRepository;

    public GetRequestHandler(IEmailTemplateRepository templateRepository)
    {
      fTemplateRepository = templateRepository;
    }

    public async Task<EmailTemplateDetailModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
      var user = await fTemplateRepository.GetAsync(request.TemplateId, cancellationToken);
      if (user is null)
        throw new NotFoundException($"Email template with ID '{request.TemplateId}' does not exist.");

      return user.ToDetail();
    }
  }
}
