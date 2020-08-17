using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Models.ConnectToken
{
  public class RefreshLoginPostModel
  {
    [Required]
    public string RefreshToken { get; set; } = default!;
  }
}
