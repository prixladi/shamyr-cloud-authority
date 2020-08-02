using Shamyr.Cloud.Database.Documents;

namespace Shamyr.Cloud.Authority.Service.Dtos.Clients
{
  public class ClientUpdateDto
  {
    public string ClientName { get; set; } = default!;
    public SecretDoc ClientSecret { get; set; } = default!;
  }
}
