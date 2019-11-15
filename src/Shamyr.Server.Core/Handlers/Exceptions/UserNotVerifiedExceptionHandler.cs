using Shamyr.AspNetCore.Handlers.Exceptions;
using Shamyr.AspNetCore.Models;
using Shamyr.Server.Models;

namespace Shamyr.Server.Handlers.Exceptions
{
  public class UserNotVerifiedExceptionHandler: ExceptionHandlerBase<EmailNotVerifiedException>
  {
    protected override int DoGetStatusCode(EmailNotVerifiedException exception)
    {
      return CustomStatusCodes.Status430EmailNotVerified;
    }

    protected override MessageResponseModel DoCreateResponseModel(EmailNotVerifiedException exception)
    {
      return new EmailNotVerifiedResponseModel(exception.Message, exception.EmailPrincipal);
    }
  }
}
