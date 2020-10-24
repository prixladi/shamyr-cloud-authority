using System;
using Shamyr.Cloud.Authority.Service.Models.Clients;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class ClientPostModelExtensions
  {
    public static ClientDoc ToDoc(this PostModel model, SecretDoc? secret)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      return new ClientDoc
      {
        Name = model.Name,
        PasswordResetEmailTemplateId = model.PasswordResetEmailTemplateId,
        VerifyAccountEmailTemplateId = model.VerifyAccountEmailTemplateId,
        AuthorityUrl = model.AuthorityUrl,
        PortalUrl = model.PortalUrl,
        Secret = secret
      };
    }
  }
}
