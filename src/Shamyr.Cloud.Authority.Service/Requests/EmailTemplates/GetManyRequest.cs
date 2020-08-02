using System.Collections.Generic;
using MediatR;
using Shamyr.Cloud.Authority.Service.Models.Clients;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Requests.EmailTemplates
{
  public class GetManyRequest: IRequest<ICollection<EmailTemplatePreviewModel>>
  {
  }
}
