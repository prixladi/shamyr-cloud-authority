using MediatR;
using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Notifications.Users
{
  public class LoggedOutNotification: INotification
  {
    public ObjectId UserId { get; }

    public LoggedOutNotification(ObjectId userId)
    {
      UserId = userId;
    }
  }
}
