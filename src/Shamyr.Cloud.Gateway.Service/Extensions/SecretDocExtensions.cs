using System;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Security;

namespace Shamyr.Cloud.Gateway.Service.Extensions
{
  public static class SecretDocExtensions
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
  }
}
