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
        NormalizedUsername = payload.Email.Normalize(),
        Username = payload.Email,
        NormalizedEmail = payload.Email.Normalize(),
        Email = payload.Email
      };
    }
  }
}
