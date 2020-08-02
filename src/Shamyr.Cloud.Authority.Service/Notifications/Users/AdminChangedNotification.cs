using MediatR;
using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Notifications.Users
{
  public class AdminChangedNotification: INotification
  {
    public ObjectId UserId { get; }
    public bool Admin { get; }

    public AdminChangedNotification(ObjectId userId, bool admin)
    {
      UserId = userId;
      Admin = admin;
    }
  }
}
