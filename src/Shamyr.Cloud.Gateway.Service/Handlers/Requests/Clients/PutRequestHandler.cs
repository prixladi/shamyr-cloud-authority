using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Dtos.Clients;
using Shamyr.Cloud.Gateway.Service.Extensions.Models;
using Shamyr.Cloud.Gateway.Service.Repositories.Clients;
using Shamyr.Cloud.Gateway.Service.Requests.Clients;
using Shamyr.Cloud.Services;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.Clients
{
  public class PutRequestHandler: IRequestHandler<PutRequest>
  {
    private readonly IClientRepository fClientRepository;
    private readonly ISecretService fSecretService;

    public PutRequestHandler(IClientRepository clientRepository, ISecretService secretService)
    {
      fClientRepository = clientRepository;
      fSecretService = secretService;
    }

    public async Task<Unit> Handle(PutRequest request, CancellationToken cancellationToken)
    {
      var client = await fClientRepository.GetAsync(request.ClientId, cancellationToken);
      if (client is null)
        throw new NotFoundException($"Client with ID '{request.ClientId}' does not exist.");

      if (client.ClientName != request.Model.ClientName && await fClientRepository.ExistsByClientNameAsync(request.Model.ClientName, cancellationToken))
        throw new ConflictException($"Client with clientId '{request.Model.ClientName}' already exists.");

      var secret = fSecretService.CreateSecret(request.Model.ClientSecret);
      var updateDto = new ClientUpdateDto
      {
        ClientName = request.Model.ClientName,
        ClientSecret = secret.ToDoc()
      };

      await fClientRepository.UpdateAsync(request.ClientId, updateDto, cancellationToken);

      return Unit.Value;
    }
  }
}
