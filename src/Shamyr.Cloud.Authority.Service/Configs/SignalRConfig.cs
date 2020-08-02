
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace Shamyr.Cloud.Authority.Service.Configs
{
  public static class SignalRConfig
  {
    public static void SetupJsonProtocol(JsonHubProtocolOptions options)
    {
      options.PayloadSerializerOptions = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      };
    }
  }
}
