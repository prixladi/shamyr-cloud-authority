using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models.Clients;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Clients;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Clients
{
  public class GetManyRequestHandler: IRequestHandler<GetManyRequest, ICollection<PreviewModel>>
  {
    private readonly IClientRepository fClientRepository;

    public GetManyRequestHandler(IClientRepository clientRepository)
    {
      fClientRepository = clientRepository;
    }

    public async Task<ICollection<PreviewModel>> Handle(GetManyRequest request, CancellationToken cancellationToken)
    {
      var docs = await fClientRepository.GetAsync(cancellationToken);
      return docs.ToPreview();
    }
  }
}
