﻿using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Dtos.Clients
{
  public record UpdateDto(
    string Name,
    SecretDoc? Secret,
    ObjectId? VerifyAccountEmailTemplateId,
    ObjectId? PasswordResetEmailTemplateId,
    string? AuthorityUrl,
    string? PortalUrl);
}
