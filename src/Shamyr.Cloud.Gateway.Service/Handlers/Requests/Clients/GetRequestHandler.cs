using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Extensions.Models;
using Shamyr.Cloud.Gateway.Service.Models.Clients;
using Shamyr.Cloud.Gateway.Service.Repositories.Clients;
using Shamyr.Cloud.Gateway.Service.Requests.Clients;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.Clients
{
  public class GetRequestHandler: IRequestHandler<GetRequest, ClientDetailModel>
  {
    private readonly IClientRepository fClientRepository;

    public GetRequestHandler(IClientRepository clientRepository)
    {
      fClientRepository = clientRepository;
    }

    public async Task<ClientDetailModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
      var user = await fClientRepository.GetAsync(request.ClientId, cancellationToken);
      if (user is null)
        throw new NotFoundException($"Cliet with ID '{request.ClientId}' does not exist.");

      return user.ToDetailModel();
    }
  }
}
