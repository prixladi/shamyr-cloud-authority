using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Gateway.Service.Models.Users
{
  public class UserPreviewsModel
  {
    [Required]
    public ICollection<UserPreviewModel> UserPreviews { get; set; } = default!;

    [Required]
    public int UserCount { get; set; }
  }
}
