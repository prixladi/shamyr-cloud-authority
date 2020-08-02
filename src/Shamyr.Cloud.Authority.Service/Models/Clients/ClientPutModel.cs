using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Shamyr.Cloud.Authority.Service.Models.Clients
{
  public class ClientPutModel
  {
    [StringLength(50, MinimumLength = 5)]
    [Required]
    public string ClientName { get; set; } = default!;

    [StringLength(int.MaxValue, MinimumLength = 5)]
    [Required]
    public string ClientSecret { get; set; } = default!;
  }
}
