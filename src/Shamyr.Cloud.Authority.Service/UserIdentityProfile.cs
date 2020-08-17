using System;
using System.Security.Claims;
using MongoDB.Bson;
using Shamyr.Security;

namespace Shamyr.Cloud.Authority.Service
{
  public class UserIdentityProfile: ClaimsIdentity
  {
    public UserIdentityProfile(ObjectId userId, ClaimsIdentity other)
      : base(other)
    {
      UserId = userId;
    }

    public ObjectId UserId { get; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Secret? Secret { get; set; } = default!;
    public DateTime? LogoutUtc { get; set; }
    public bool Admin { get; set; }

    public UserIdentity Base => new UserIdentity(id: UserId.ToString(), username: Username, email: Email);
  }
}
