using MediatR;
using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Notifications.Users
{
  public class DisabledChangedNotification: INotification
  {
    public ObjectId UserId { get; }
    public bool Disabled { get; }

    public DisabledChangedNotification(ObjectId userId, bool disabled)
    {
      UserId = userId;
      Disabled = disabled;
    }
  }
}
