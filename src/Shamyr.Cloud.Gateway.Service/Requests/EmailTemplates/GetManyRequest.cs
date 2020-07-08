using System.Collections.Generic;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Models.Clients;
using Shamyr.Cloud.Gateway.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Gateway.Service.Requests.EmailTemplates
{
  public class GetManyRequest: IRequest<ICollection<EmailTemplatePreviewModel>>
  {
  }
}
