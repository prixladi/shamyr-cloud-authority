using System.Collections.Generic;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Models.Clients;

namespace Shamyr.Cloud.Gateway.Service.Requests.Clients
{
  public class GetManyRequest: IRequest<ICollection<ClientPreviewModel>>
  {
  }
}
