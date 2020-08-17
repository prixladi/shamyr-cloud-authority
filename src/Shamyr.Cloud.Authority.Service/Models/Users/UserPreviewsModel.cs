using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public class UserPreviewsModel
  {
    [Required]
    public IReadOnlyCollection<UserPreviewModel> UserPreviews { get; set; } = default!;

    [Required]
    public int UserCount { get; set; }
  }
}
