using MediatR;
using Shamyr.Cloud.Gateway.Service.Models;
using Shamyr.Cloud.Gateway.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Gateway.Service.Requests.EmailTemplates
{
  public class PostRequest: IRequest<IdModel>
  {
    public EmailTemplatePostModel Model { get; }

    public PostRequest(EmailTemplatePostModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
