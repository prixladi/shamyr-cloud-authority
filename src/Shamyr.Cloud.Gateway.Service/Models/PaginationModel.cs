using System.ComponentModel.DataAnnotations;

namespace Shamyr.Cloud.Gateway.Service.Models
{
  public class PaginationModel
  {
    [Range(0, int.MaxValue)]
    [Required]
    public int Skip { get; set; }

    [Range(1, 100)]
    [Required]
    public int Take { get; set; }
  }
}
