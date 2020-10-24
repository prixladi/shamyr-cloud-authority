using System;
using System.Collections.Generic;
using System.Linq;
using Shamyr.Cloud.Authority.Service.Models.Clients;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class ClientDocExtensions
  {
    public static PreviewModel ToPreview(this ClientDoc client)
    {
      if (client is null)
        throw new ArgumentNullException(nameof(client));

      return new PreviewModel
      {
        Id = client.Id,
        Name = client.Name,
        Disabled = client.Disabled,
      };
    }

    public static ICollection<PreviewModel> ToPreview(this IEnumerable<ClientDoc> clients)
    {
      if (clients is null)
        throw new ArgumentNullException(nameof(clients));

      return clients.Select(ToPreview).ToList();
    }

    public static DetailModel ToDetail(this ClientDoc client)
    {
      if (client is null)
        throw new ArgumentNullException(nameof(client));

      return new DetailModel
      {
        Id = client.Id,
        Name = client.Name,
        Disabled = client.Disabled,
        PasswordResetEmailTemplateId = client.PasswordResetEmailTemplateId,
        VerifyAccountEmailTemplateId = client.VerifyAccountEmailTemplateId,
        AuthorityUrl = client.AuthorityUrl,
        PortalUrl = client.PortalUrl
      };
    }
  }
}
