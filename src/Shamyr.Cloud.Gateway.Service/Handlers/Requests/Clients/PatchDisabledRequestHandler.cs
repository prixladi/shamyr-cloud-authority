using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Repositories.Clients;
using Shamyr.Cloud.Gateway.Service.Requests.Clients;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.Clients
{
  public class PatchDisabledRequestHandler: IRequestHandler<PatchDisabledRequest>
  {
    private readonly IClientRepository fClientRepository;

    public PatchDisabledRequestHandler(IClientRepository clientRepository)
    {
      fClientRepository = clientRepository;
    }

    public async Task<Unit> Handle(PatchDisabledRequest request, CancellationToken cancellationToken)
    {
      if (!await fClientRepository.TrySetDisabledAsync(request.ClientId, request.Model.Disabled, cancellationToken))
        throw new NotFoundException($"Client with ID '{request.ClientId}' does not exist.");

      return Unit.Value;
    }
  }
}
