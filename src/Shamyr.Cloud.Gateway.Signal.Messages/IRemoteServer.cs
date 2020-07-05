using System.Threading.Tasks;

namespace Shamyr.Cloud.Gateway.Signal.Messages
{
  public interface IRemoteServer
  {
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<SubscribeIdentityResourcesResponse> SubscribeResourceAsync(SubscribeIdentityResourcesRequest request);
  }
}
