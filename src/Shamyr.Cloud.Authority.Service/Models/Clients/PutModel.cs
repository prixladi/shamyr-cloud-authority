using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Shamyr.Cloud.Authority.Service.Models.Clients
{
  public record PutModel
  {
    [StringLength(ModelConstants._MaxClientNameLength, MinimumLength = ModelConstants._MinClientNameLength)]
    [Required]
    public string Name { get; init; } = default!;

    [StringLength(ModelConstants._MaxSecretLength, MinimumLength = ModelConstants._MaxClientNameLength)]
    [Required]
    public string? Secret { get; init; }

    public bool RequireEmailVerification { get; set; }

    public ObjectId? VerifyAccountEmailTemplateId { get; init; }

    public ObjectId? PasswordResetEmailTemplateId { get; init; }

    [Url]
    public string? AuthorityUrl { get; init; }

    [Url]
    public string? PortalUrl { get; init; }
  }
}
