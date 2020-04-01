using System;
using Shamyr.Database.Mongo;
using Shamyr.Database.Mongo.Attributes;

namespace Shamyr.Server.Database.Documents
{
  [MongoCollection(nameof(DbCollections.Users))]
  public class UserDoc: DocumentBase
  {
    [Indexed(Unique = true)]
    public string Username { get; set; } = default!;

    [Indexed(Unique = true)]
    public string NormalizedUsername { get; set; } = default!;

    [Indexed(Unique = true)]
    public string NormalizedEmail { get; set; } = default!;

    [Indexed(Unique = true)]
    public string Email { get; set; } = default!;

    public SecretDoc Secret { get; set; } = default!;

    public string? EmailToken { get; set; }

    public string? PasswordToken { get; set; }

    public bool Disabled { get; set; }

    public UserTokenDoc? RefreshToken { get; set; }

    public DateTime? LogoutUtc { get; set; }

    public UserPermissionDoc? UserPermission { get; set; }
  }
}
