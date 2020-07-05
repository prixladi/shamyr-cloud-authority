using Shamyr.MongoDB.Indexes;

namespace Shamyr.Cloud.Database.Documents
{
  public class UserPermissionDoc
  {
    [Index]
    public PermissionKind Kind { get; set; }
  }
}
