using System.Threading.Tasks;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public interface IRemoteServer
  {
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<SubscribeEventsResponse> SubscribeResourceAsync(SubscribeEventsRequest request);
  }
}
