using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Extensions;
using Shamyr.Cloud.Gateway.Service.Repositories;
using Shamyr.Cloud.Gateway.Service.SignalR.Hubs;
using Shamyr.Cloud.Gateway.Signal.Messages.Identity;
using Shamyr.Cloud.Services;

namespace Shamyr.Cloud.Gateway.Service.Services
{
  public class ClientService: IClientService
  {
    private readonly IHubContext<ClientHub> fHubContext;
    private readonly IClientRepository fClientRepository;
    private readonly ISecretService fSecretService;

    public ClientService(
      IHubContext<ClientHub> hubContext,
      IClientRepository clientRepository,
      ISecretService secretService)
    {
      fHubContext = hubContext;
      fClientRepository = clientRepository;
      fSecretService = secretService;
    }

    public async Task<ClientLoginStatus> LoginAsync(ObjectId clientId, string clientSecret, CancellationToken cancellationToken)
    {
      var client = await fClientRepository.GetAsync(clientId, cancellationToken);
      if (client is null)
        return ClientLoginStatus.ClientNotFound;

      if (!fSecretService.ComparePasswords(clientSecret, client.Secret.ToModel()))
        return ClientLoginStatus.InvalidSecret;

      return ClientLoginStatus.Ok;
    }

    public async Task SubscribeToResourcesAsync(string[] resources, string connectionId, CancellationToken cancellationToken)
    {
      var allowedResources = typeof(Resources)
        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
        .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
        .Select(x => (string)x.GetRawConstantValue()!)
        .ToArray();

      var invalidResources = resources
        .Where(res => !allowedResources.Contains(res))
        .ToArray();

      if (invalidResources.Length > 0)
        throw new BadRequestException($"Resources '{string.Join((","), invalidResources)}' are invalid.");

      foreach (var resource in resources)
        await fHubContext.Groups.AddToGroupAsync(connectionId, resource, cancellationToken);
    }
  }
}
