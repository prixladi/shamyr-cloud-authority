using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Extensions;
using Shamyr.Cloud.Gateway.Service.Models.Clients;
using Shamyr.Cloud.Gateway.Service.Repositories.Clients;
using Shamyr.Cloud.Gateway.Service.Requests.Clients;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.Clients
{
  public class GetManyRequestHandler: IRequestHandler<GetManyRequest, ICollection<ClientPreviewModel>>
  {
    private readonly IClientRepository fClientRepository;

    public GetManyRequestHandler(IClientRepository clientRepository)
    {
      fClientRepository = clientRepository;
    }

    public async Task<ICollection<ClientPreviewModel>> Handle(GetManyRequest request, CancellationToken cancellationToken)
    {
      var docs = await fClientRepository.GetAsync(cancellationToken);
      return docs.ToModel();
    }
  }
}
