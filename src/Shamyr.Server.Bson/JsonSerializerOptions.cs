using System.Text.Json;

namespace Shamyr.Server
{
  public static class JsonSerializerOptions
  {
    public static System.Text.Json.JsonSerializerOptions WithObjectIdConveter
    {
      get
      {
        var options = new System.Text.Json.JsonSerializerOptions()
        {
          PropertyNameCaseInsensitive = false,
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        options.Converters.Add(new ObjectIdJsonConverter());

        return options;
      }
    }
  }
}
