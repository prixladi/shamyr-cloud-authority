using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models.Clients;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Clients;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Clients
{
  public class GetRequestHandler: IRequestHandler<GetRequest, DetailModel>
  {
    private readonly IClientRepository fClientRepository;

    public GetRequestHandler(IClientRepository clientRepository)
    {
      fClientRepository = clientRepository;
    }

    public async Task<DetailModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
      var client = await fClientRepository.GetAsync(request.ClientId, cancellationToken);
      if (client is null)
        throw new NotFoundException($"Cliemt with ID '{request.ClientId}' does not exist.");

      return client.ToDetail();
    }
  }
}
