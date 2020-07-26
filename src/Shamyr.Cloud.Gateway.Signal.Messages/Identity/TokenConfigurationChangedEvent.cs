using System.Text.Json.Serialization;
using Shamyr.Cloud.Gateway.Token.Models;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Signal.Messages.Identity
{
  public class TokenConfigurationChangedEvent: IdentityEventBase
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
