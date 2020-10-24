using Shamyr.Cloud.Authority.Service.Repositories;

namespace Shamyr.Cloud.Authority.Service.Dtos.Users
{
  public record FilterDto(
    int Skip,
    int Take,
    string? Username,
    string? Email,
    bool? Admin): IPagination;
}
