using System;
using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Dtos
{
  public class ValidatedJwtDto
  {
    public ObjectId UserId { get; set; }
    public DateTime IssuedAtUtc { get; set; }
  }
}
