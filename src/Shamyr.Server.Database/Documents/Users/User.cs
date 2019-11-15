using System;
using Shamyr.Database.Mongo;
using Shamyr.Database.Mongo.Attributes;
using Shamyr.Server.Database.Documents.Permissions;

namespace Shamyr.Server.Database.Documents.Users
{
  [MongoCollection(nameof(DbCollections.Users))]
  public class User: DocumentBase
  {
    [Indexed(Unique = true)]
    public string Username { get; set; } = default!;

    [Indexed(Unique = true)]
    public string NormalizedUsername { get; set; } = default!;

    [Indexed(Unique = true)]
    public string NormalizedEmail { get; set; } = default!;

    [Indexed(Unique = true)]
    public string Email { get; set; } = default!;

    public UserSecret Secret { get; set; } = default!;

    public string? EmailToken { get; set; } 

    public string? PasswordToken { get; set; }

    public bool Disabled { get; set; }

    public UserToken? RefreshToken { get; set; } 

    public DateTime? LogoutUtc { get; set; }

    public DiscordPermission? DiscordPermission { get; set; }

    public UserPermission? UserPermission { get; set; }
  }
}
