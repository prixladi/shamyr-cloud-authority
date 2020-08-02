using Shamyr.Cloud.Authority.Signal.Messages;

namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public class UserQueryFilter: PaginationModel
  {
    public string? Username { get; set; }
    public string? Email { get; set; }
    public bool? Admin { get; set; }
  }
}
