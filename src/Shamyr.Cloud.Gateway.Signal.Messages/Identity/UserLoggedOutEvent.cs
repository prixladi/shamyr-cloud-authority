using System.Text.Json.Serialization;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages.Identity
{
  public class UserLoggedOutEvent: IdentityUserEventMessageBase
  {
    public override string Resource => Resources._UserLoggedOut;

    [JsonConstructor]
    public UserLoggedOutEvent(string userId, string operationId, string? parentOperationId)
      : base(userId, operationId, parentOperationId) { }

    [JsonConstructor]
    public UserLoggedOutEvent(string userId, IOperationContext operationContext)
      : base(userId, operationContext) { }
  }
}
