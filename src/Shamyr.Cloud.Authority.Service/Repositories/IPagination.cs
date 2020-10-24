namespace Shamyr.Cloud.Authority.Service.Repositories
{
  public interface IPagination
  {
    public int Skip { get; }
    public int Take { get; }
  }
}
