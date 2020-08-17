using System.Text.Json.Serialization;
using Shamyr.Cloud.Authority.Models;
using Shamyr.Logging;

namespace Shamyr.Cloud.Authority.Signal.Messages
{
  public class TokenConfigurationChangedEvent: EventMessageBase
  {
    public TokenConfigurationModel Model { get; }

    [JsonConstructor]
    public TokenConfigurationChangedEvent(TokenConfigurationModel model, string operationId, string? parentOperationId)
      : base(operationId, parentOperationId)
    {
      Model = model;
    }

    public TokenConfigurationChangedEvent(TokenConfigurationModel model, ILoggingContext context)
      : base(context)
    {
      Model = model;
    }
  }
}
