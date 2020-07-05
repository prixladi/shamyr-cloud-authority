using MediatR;
using MongoDB.Bson;

namespace Shamyr.Cloud.Gateway.Service.Notifications.Users
{
  public class UserDisabledStatusChangedNotification: INotification
  {
    public ObjectId UserId { get; }
    public bool Disabled { get; }

    public UserDisabledStatusChangedNotification(ObjectId userId, bool disabled)
    {
      UserId = userId;
      Disabled = disabled;
    }
  }
}
