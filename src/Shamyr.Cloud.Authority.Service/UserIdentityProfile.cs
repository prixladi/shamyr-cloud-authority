using System;
using System.Security.Claims;
using MongoDB.Bson;
using Shamyr.Security;

namespace Shamyr.Cloud.Authority.Service
{
  public class UserIdentityProfile: ClaimsIdentity
  {
    public ObjectId UserId { get; }
    public string? Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string? GivenName { get; init; } = default!;
    public string? FamilyName { get; init; } = default!;
    public Secret? Secret { get; init; } = default!;
    public DateTime? LogoutUtc { get; init; }
    public bool Admin { get; init; }

    public UserIdentityProfile(ObjectId userId, ClaimsIdentity other)
      : base(other)
    {
      UserId = userId;
    }

    public UserIdentity Base => new UserIdentity(id: UserId.ToString(), email: Email, username: Username);
  }
}
