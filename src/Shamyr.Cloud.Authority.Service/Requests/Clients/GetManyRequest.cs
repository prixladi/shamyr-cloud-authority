using System.Collections.Generic;
using MediatR;
using Shamyr.Cloud.Authority.Service.Models.Clients;

namespace Shamyr.Cloud.Authority.Service.Requests.Clients
{
  public class GetManyRequest: IRequest<ICollection<PreviewModel>>
  {
  }
}
