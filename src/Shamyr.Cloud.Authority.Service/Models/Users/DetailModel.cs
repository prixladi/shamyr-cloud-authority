using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public record DetailModel
  {
    [Required]
    public ObjectId Id { get; init; }

    [Required]
    public string Username { get; init; } = default!;

    public string? GivenName { get; init; } = default!;

    public string? FamilyName { get; init; } = default!;

    [Required]
    public string Email { get; init; } = default!;

    [Required]
    public bool Disabled { get; init; }

    [Required]
    public bool Admin { get; init; } = default!;
  }
}
