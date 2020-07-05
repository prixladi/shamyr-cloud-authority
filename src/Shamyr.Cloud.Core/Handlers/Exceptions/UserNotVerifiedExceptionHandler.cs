using Shamyr.AspNetCore.Handlers.Exceptions;
using Shamyr.AspNetCore.Models;
using Shamyr.Cloud.Models;

namespace Shamyr.Cloud.Handlers.Exceptions
{
  public class UserNotVerifiedExceptionHandler: ExceptionHandlerBase<EmailNotVerifiedException>
  {
    protected override int DoGetStatusCode(EmailNotVerifiedException exception)
    {
      return CustomStatusCodes.Status430NotVerified;
    }

    protected override MessageResponseModel DoCreateResponseModel(EmailNotVerifiedException exception)
    {
      return new EmailNotVerifiedResponseModel(exception.Message, exception.EmailPrincipal);
    }
  }
}
