namespace Shamyr.Cloud.Authority.Service.SignalR.Hubs
{
  public enum ClientLoginStatus
  {
    Ok,
    ClientNotFound,
    SecretNotSet,
    InvalidSecret,
  }
}
