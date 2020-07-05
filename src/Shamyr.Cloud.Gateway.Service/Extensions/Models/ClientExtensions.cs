using System;
using System.Collections.Generic;
using System.Linq;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Models.Clients;

namespace Shamyr.Cloud.Gateway.Service.Extensions.Models
{
  public static class ClientExtensions
  {
    public static ClientPreviewModel ToModel(this ClientDoc client)
    {
      if (client is null)
        throw new ArgumentNullException(nameof(client));

      return new ClientPreviewModel
      {
        Id = client.Id,
        ClientName = client.ClientName,
        Disabled = client.Disabled,
      };
    }

    public static ICollection<ClientPreviewModel> ToModel(this List<ClientDoc> clients)
    {
      if (clients is null)
        throw new ArgumentNullException(nameof(clients));

      return clients.Select(ToModel).ToList();
    }

    public static ClientDetailModel ToDetailModel(this ClientDoc client)
    {
      if (client is null)
        throw new ArgumentNullException(nameof(client));

      return new ClientDetailModel
      {
        Id = client.Id,
        ClientName = client.ClientName,
        Disabled = client.Disabled
      };
    }
  }
}
