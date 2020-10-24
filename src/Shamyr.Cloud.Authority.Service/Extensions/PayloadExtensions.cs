using System;
using Shamyr.Cloud.Database.Documents;

using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Shamyr.Cloud.Authority.Service.Extensions
{
  public static class PayloadExtensions
  {
    public static UserDoc ToUserDoc(this Payload payload)
    {
      if (payload is null)
        throw new ArgumentNullException(nameof(payload));

      return new UserDoc
      {
        NormalizedUsername = payload.Name.CompareNormalize(),
        Username = payload.Name,
        NormalizedEmail = payload.Email.CompareNormalize(),
        Email = payload.Email,
        GivenName = payload.GivenName,
        FamilyName = payload.FamilyName
      };
    }
  }
}
