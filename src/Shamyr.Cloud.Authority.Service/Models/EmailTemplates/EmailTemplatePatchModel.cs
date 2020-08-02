using System.Text.Json.Serialization;

namespace Shamyr.Cloud.Authority.Service.Models.EmailTemplates
{
  public class EmailTemplatePatchModel
  {
    [JsonPropertyName("op")]
    public string Operation { get; set; } = default!;

    public string Property { get; set; } = default!;

    public string Value { get; set; } = default!;
  }
}
