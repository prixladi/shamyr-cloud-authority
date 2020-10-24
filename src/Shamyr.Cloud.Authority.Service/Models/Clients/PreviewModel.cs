using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Shamyr.Cloud.Authority.Service.Models.Clients
{
  public record PreviewModel
  {
    [Required]
    public ObjectId Id { get; init; }

    [Required]
    public string Name { get; init; } = default!;

    [Required]
    public bool Disabled { get; init; }
  }
}
