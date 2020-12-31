namespace Shamyr.Cloud.Authority.Service.Dtos.Users
{
  public record UpdateDto(
    string Username,
    string? GivenName,
    string? FamilyName);
}
