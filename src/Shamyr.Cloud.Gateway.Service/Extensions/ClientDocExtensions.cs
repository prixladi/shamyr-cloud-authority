using System;
using System.Collections.Generic;
using System.Linq;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Models.Clients;

namespace Shamyr.Cloud.Gateway.Service.Extensions
{
  public static class ClientDocExtensions
  {
    public static ClientPreviewModel ToPreview(this ClientDoc client)
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

    public static ICollection<ClientPreviewModel> ToPreview(this IEnumerable<ClientDoc> clients)
    {
      if (clients is null)
        throw new ArgumentNullException(nameof(clients));

      return clients.Select(ToPreview).ToList();
    }

    public static ClientDetailModel ToDetail(this ClientDoc client)
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
