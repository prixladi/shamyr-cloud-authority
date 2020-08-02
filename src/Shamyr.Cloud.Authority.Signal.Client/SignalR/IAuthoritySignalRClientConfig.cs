namespace Shamyr.Cloud.Authority.Client.SignalR
{
  public interface IAuthoritySignalRClientConfig: IAuthorityClientConfig
  {
    string ClientId { get; }
    string ClientSecret { get; }

    public string[] SubscribedResources { get; }
  }
}
