using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.SignalR.Hubs;

namespace Shamyr.Cloud.Gateway.Service.Services
{
  public interface IClientService
  {
    Task<ClientLoginStatus> LoginAsync(ObjectId clientId, string clientSecret, CancellationToken cancellationToken);
    Task SubscribeToResourcesAsync(string[] resources, string connectionId, CancellationToken cancellationToken);
  }
}