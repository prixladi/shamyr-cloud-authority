using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Models.Clients
{
  public record DetailModel
  {
    [Required]
    public ObjectId Id { get; init; }

    [Required]
    public string Name { get; init; } = default!;

    [Required]
    public bool Disabled { get; init; }

    public bool RequireEmailVerification { get; set; }

    public ObjectId? VerifyAccountEmailTemplateId { get; init; }

    public ObjectId? PasswordResetEmailTemplateId { get; init; }

    [Url]
    public string? AuthorityUrl { get; init; }

    [Url]
    public string? PortalUrl { get; init; }
  }
}
