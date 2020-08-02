using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Cloud.Authority.Token.Models;
using Shamyr.Text.Json;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Authority.Client.Services
{
  public class TokenConfigurationService: ITokenConfigurationService
  {
    public async Task<TokenConfigurationModel> GetAsync(Uri authorityUrl, IOperationContext context, CancellationToken cancellationToken)
    {
      var url = authorityUrl.AppendPath("token/configuration.json");
      var client = HttpClientContext.Client;
      var result = await client.GetAsync(url);
      result.EnsureSuccessStatusCode();

      var body = await result.Content.ReadAsStringAsync(cancellationToken);
      return await JsonConvert.DeserializeAsync<TokenConfigurationModel>(body, new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      },cancellationToken);
    }
  }
}
