using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models;
using Shamyr.Cloud.Authority.Service.Repositories;
using Shamyr.Cloud.Authority.Service.Requests.Clients;
using Shamyr.Cloud.Services;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.Clients
{
  public class PostRequestHandler: IRequestHandler<PostRequest, IdModel>
  {
    private readonly IClientRepository fClientRepository;
    private readonly ISecretService fSecretService;

    public PostRequestHandler(IClientRepository clientRepository, ISecretService secretService)
    {
      fClientRepository = clientRepository;
      fSecretService = secretService;
    }

    public async Task<IdModel> Handle(PostRequest request, CancellationToken cancellationToken)
    {
      if (await fClientRepository.ExistsByClientNameAsync(request.Model.ClientName, cancellationToken))
        throw new ConflictException($"Client with name '{request.Model.ClientName}' already exists.");

      var secret = fSecretService.CreateSecret(request.Model.ClientSecret);
      var clientDoc = new ClientDoc
      {
        ClientName = request.Model.ClientName,
        Secret = secret.ToDoc(),
        Disabled = false
      };

      await fClientRepository.InsertAsync(clientDoc, cancellationToken);

      return new IdModel(clientDoc.Id);
    }
  }
}
