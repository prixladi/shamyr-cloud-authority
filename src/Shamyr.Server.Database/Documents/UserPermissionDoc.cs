using Shamyr.Database.Mongo.Attributes;

namespace Shamyr.Server.Database.Documents
{
  public class UserPermissionDoc
  {
    [Indexed]
    public PermissionKind Kind { get; set; }
  }
}
