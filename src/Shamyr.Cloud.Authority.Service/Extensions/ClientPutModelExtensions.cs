using System;
using Shamyr.Cloud.Authority.Service.Dtos.Clients;
using Shamyr.Cloud.Authority.Service.Models.Clients;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class ClientPutModelExtensions
  {
    public static UpdateDto ToDto(this PutModel model, SecretDoc? secret)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      return new UpdateDto(
        Name: model.Name,
        RequireEmailVerification: model.RequireEmailVerification,
        VerifyAccountEmailTemplateId: model.VerifyAccountEmailTemplateId,
        PasswordResetEmailTemplateId: model.PasswordResetEmailTemplateId,
        AuthorityUrl: model.AuthorityUrl,
        PortalUrl: model.PortalUrl,
        Secret: secret);
    }
  }
}
