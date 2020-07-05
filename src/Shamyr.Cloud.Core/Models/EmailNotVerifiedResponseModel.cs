using Shamyr.AspNetCore.Models;

namespace Shamyr.Cloud.Models
{
  public class EmailNotVerifiedResponseModel: MessageResponseModel
  {
    public object User { get; }

    public EmailNotVerifiedResponseModel(string message, object user)
      : base(message)
    {
      User = user;
    }
  }
}
