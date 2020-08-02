using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Shamyr.Cloud.Authority.Service.Models.Clients
{
  public class ClientPreviewModel
  {
    [Required]
    public ObjectId Id { get; set; }

    [Required]
    public string ClientName { get; set; } = default!;

    [Required]
    public bool Disabled { get; set; }
  }
}
