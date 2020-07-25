using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Models.Logins;

namespace Shamyr.Cloud.Gateway.Service.Extensions
{
  public static class TokenDocExtensions
  {
    public static TokensModel ToTokensModel(this TokenDoc doc, string jwt)
    {
      if (doc is null)
        throw new ArgumentNullException(nameof(doc));
      if (jwt is null)
        throw new ArgumentNullException(nameof(jwt));

      return new TokensModel
      {
        BearerToken = jwt,
        RefreshToken = doc.Value,
        RefreshTokenExpirationUtc = doc.ExpirationUtc
      };
    }
  }
}
