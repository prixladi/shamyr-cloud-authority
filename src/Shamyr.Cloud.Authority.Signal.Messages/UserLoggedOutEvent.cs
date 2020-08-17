using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class UserLoggedOutEvent: UserEventBase
  {
    public override string Resource => Resources._UserLoggedOut;

    [JsonConstructor]
    public UserLoggedOutEvent(string userId, string operationId, string? parentOperationId)
      : base(userId, operationId, parentOperationId) { }


    public UserLoggedOutEvent(string userId, ILoggingContext operationContext)
      : base(userId, operationContext) { }
  }
}
