using System;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Security;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class SecretExtensions
  {
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
