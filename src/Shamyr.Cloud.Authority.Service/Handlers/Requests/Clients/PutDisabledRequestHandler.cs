using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Clients;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Clients
{
  public class PutDisabledRequestHandler: IRequestHandler<PutDisabledRequest>
  {
    private readonly IClientRepository fClientRepository;

    public PutDisabledRequestHandler(IClientRepository clientRepository)
    {
      fClientRepository = clientRepository;
    }

    public async Task<Unit> Handle(PutDisabledRequest request, CancellationToken cancellationToken)
    {
      if (!await fClientRepository.TrySetDisabledAsync(request.ClientId, request.Model.Disabled, cancellationToken))
        throw new NotFoundException($"Client with ID '{request.ClientId}' does not exist.");

      return Unit.Value;
    }
  }
}
