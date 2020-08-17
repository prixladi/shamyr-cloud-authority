using System;
using System.Text.Json.Serialization;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public abstract class UserEventBase: EventMessageBase
  {
    [JsonIgnore]
    public abstract string Resource { get; }

    public string UserId { get; set; }

    protected UserEventBase(string userId, string operationId, string? parentOperationId)
      : base(operationId, parentOperationId)
    {
      UserId = userId ?? throw new ArgumentNullException(nameof(userId));
    }

    protected UserEventBase(string userId, ILoggingContext context)
      : base(context)
    {
      UserId = userId ?? throw new ArgumentNullException(nameof(userId));
    }
  }
}
