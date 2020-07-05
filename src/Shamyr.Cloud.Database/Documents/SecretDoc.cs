namespace Shamyr.Cloud.Database.Documents
{
  public class SecretDoc
  {
    public string Hash { get; set; } = default!;
    public string Salt { get; set; } = default!;
  }
}
