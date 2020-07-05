using Shamyr.Cloud.Gateway.Service.SignalR;

namespace Shamyr.Cloud.Gateway.Service.Repositories.Clients
{
  public interface IClientConnectionRepository
  {
    Connection? Get(string connectionId);
    void AddOrUpdate(Connection connection);
    void Remove(string connectionId);
  }
}