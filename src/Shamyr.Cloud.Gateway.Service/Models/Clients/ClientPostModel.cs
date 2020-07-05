using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Shamyr.Cloud.Gateway.Service.Models.Clients
{
  public class ClientPostModel
  {
    [Required, JsonRequired]
    [StringLength(50, MinimumLength = 5)]
    public string ClientName { get; set; } = default!;

    [Required, JsonRequired]
    [StringLength(int.MaxValue, MinimumLength = 5)]
    public string ClientSecret { get; set; } = default!;
  }
}
