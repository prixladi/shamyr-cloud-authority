namespace Shamyr.Cloud.Authority.Service.Models.Users
{
  public record SortModel
  {
    public SortTypes? OrderBy { get; init; }
    public bool Ascending { get; init; }
  }
}
