using System.Text.Json.Serialization;
using Shamyr.Cloud.Authority.Token.Models;
using Shamyr.Tracking;

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

    public TokenConfigurationChangedEvent(TokenConfigurationModel model, IOperationContext context)
      : base(context)
    {
      Model = model;
    }
  }
}
