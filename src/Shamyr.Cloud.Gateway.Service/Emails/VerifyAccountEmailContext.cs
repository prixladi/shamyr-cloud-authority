using Shamyr.Cloud.Database.Documents;
using Shamyr.Tracking;

namespace Shamyr.Cloud.Gateway.Service.Emails
{
  public class VerifyAccountEmailContext: OperationContext, IEmailBuildContext
  {
    public EmailTemplateType EmailType => EmailTemplateType.VerifyAccount;

    public string Email { get; }
    public string VerfifyToken { get; }

    public VerifyAccountEmailContext(IOperationContext context, string email, string verfifyToken)
      :base(context)
    {
      Email = email ?? throw new System.ArgumentNullException(nameof(email));
      VerfifyToken = verfifyToken ?? throw new System.ArgumentNullException(nameof(verfifyToken));
    }
  }
}
