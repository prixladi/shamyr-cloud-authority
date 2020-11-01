using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Shamyr.Cloud.Authority.Service.Models.Clients
{
  public record PostModel
  {
    [StringLength(ModelConstants._MaxClientNameLength, MinimumLength = ModelConstants._MinClientNameLength)]
    [Required]
    public string Name { get; init; } = default!;

    [StringLength(ModelConstants._MaxSecretLength, MinimumLength = ModelConstants._MaxClientNameLength)]
    public string? Secret { get; init; } = default!;

    public ObjectId? VerifyAccountEmailTemplateId { get; init; }

    public ObjectId? PasswordResetEmailTemplateId { get; init; }

    [Url]
    public string? AuthorityUrl { get; init; }

    [Url]
    public string? PortalUrl { get; init; }
  }
}
