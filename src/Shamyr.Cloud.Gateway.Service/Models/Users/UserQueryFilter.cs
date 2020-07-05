using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service.Models.Users
{
  public class UserQueryFilter: PaginationModel
  {
    public string? Username { get; set; }
    public string? Email { get; set; }
    public PermissionKind?[]? UserPermissions { get; set; }
  }
}
