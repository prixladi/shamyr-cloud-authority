using System;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Security;

namespace Shamyr.Cloud.Gateway.Service.Extensions.Models
{
  public static class SecretExtensions
  {
    public static Secret ToModel(this SecretDoc doc)
    {
      if (doc is null)
        throw new ArgumentNullException(nameof(doc));

      return new Secret
      (
        hash: doc.Hash,
        salt: doc.Salt
      );
    }

    public static SecretDoc ToDoc(this Secret doc)
    {
      if (doc is null)
        throw new ArgumentNullException(nameof(doc));

      return new SecretDoc
      {
        Hash = doc.Hash,
        Salt = doc.Salt
      };
    }
  }
}
