using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.EmailTemplates
{
  public class PostRequestHandler: IRequestHandler<PostRequest, IdModel>
  {
    private readonly IEmailTemplateRepository fTemplateRepository;

    public PostRequestHandler(IEmailTemplateRepository templateRepository)
    {
      fTemplateRepository = templateRepository;
    }

    public async Task<IdModel> Handle(PostRequest request, CancellationToken cancellationToken)
    {
      if (await fTemplateRepository.ExistsByTypeAsync(request.Model.Type, cancellationToken))
        throw new ConflictException($"Email template with type '{request.Model.Type}' already exists.");

      var doc = request.Model.ToDoc();
      await fTemplateRepository.InsertAsync(doc, cancellationToken);

      return new IdModel(doc.Id);
    }
  }
}
