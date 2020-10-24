using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public record PreviewsModel
  {
    [Required]
    public IReadOnlyCollection<PreviewModel> UserPreviews { get; init; } = default!;

    [Required]
    public int UserCount { get; init; }
  }
}
