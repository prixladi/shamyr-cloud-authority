using MediatR;
using MongoDB.Bson;

namespace Shamyr.Cloud.Authority.Service.Notifications.Users
{
  public class VerificationChangedNotification: INotification
  {
    public ObjectId UserId { get; set; }
    public bool Verified { get; set; }

    public VerificationChangedNotification(ObjectId userId, bool verified)
    {
      UserId = userId;
      Verified = verified;
    }
  }
}
