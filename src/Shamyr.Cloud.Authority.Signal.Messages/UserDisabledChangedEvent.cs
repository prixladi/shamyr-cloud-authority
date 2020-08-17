using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class UserDisabledChangedEvent: UserEventBase
  {
    public override string Resource => Resources._UserDisabledChanged;

    public bool Disabled { get; }

    [JsonConstructor]
    public UserDisabledChangedEvent(string userId, bool disabled, string operationId, string? parentOperationId)
      : base(userId, operationId, parentOperationId)
    {
      Disabled = disabled;
    }

    public UserDisabledChangedEvent(string userId, bool disabled, ILoggingContext context)
      : base(userId, context)
    {
      Disabled = disabled;
    }
  }
}
