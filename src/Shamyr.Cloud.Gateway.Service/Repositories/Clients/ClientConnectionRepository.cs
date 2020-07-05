using System;
using System.Collections.Concurrent;
using Shamyr.Cloud.Gateway.Service.SignalR;
using Shamyr.DependencyInjection;

namespace Shamyr.Cloud.Gateway.Service.Repositories.Clients
{
  [Singleton]
  public class ClientConnectionRepository: IClientConnectionRepository
  {
    private readonly ConcurrentDictionary<string, Connection> fConnections;

    public ClientConnectionRepository()
    {
      fConnections = new ConcurrentDictionary<string, Connection>();
    }

    public Connection? Get(string connectionId)
    {
      if (connectionId is null)
        throw new ArgumentNullException(nameof(connectionId));

      if (fConnections.TryGetValue(connectionId, out var connection))
        return connection;

      return null;
    }

    public void AddOrUpdate(Connection connection)
    {
      if (connection is null)
        throw new ArgumentNullException(nameof(connection));

      fConnections.AddOrUpdate(connection.Id, connection, (_, __) => connection);
    }

    public void Remove(string connectionId)
    {
      if (connectionId is null)
        throw new ArgumentNullException(nameof(connectionId));

      fConnections.TryRemove(connectionId, out _);
    }
  }
}
