using Shamyr.MongoDB.Indexes;

namespace Shamyr.Server.Database.Documents
{
  public class UserPermissionDoc
  {
    [Index]
    public PermissionKind Kind { get; set; }
  }
}
