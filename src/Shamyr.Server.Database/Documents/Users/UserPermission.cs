using Shamyr.Database.Mongo.Attributes;
using Shamyr.Server.Common;

namespace Shamyr.Server.Database.Documents.Permissions
{
  public class UserPermission
  {
    [Indexed]
    public PermissionKind Kind { get; set; }
  }
}
