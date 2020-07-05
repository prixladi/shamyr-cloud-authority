using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shamyr.Cloud.Gateway.Service.Notifications.Users;
using Shamyr.Cloud.Gateway.Service.Requests.Emails;

namespace Shamyr.Cloud.Gateway.Service.Controllers.V1
{
  /// <summary>
  /// Controller for manupullating with emails
  /// </summary>
  [AllowAnonymous]
  [Route("api/v1/emails", Name = "Emails")]
  public class EmailsController: Controller
  {
    private readonly IMediator fMediator;

    public EmailsController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Sends email with account verification link
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Verification email has been sent</response>
    /// <response code="400">Email is invalid</response>
    /// <response code="404">Email not found</response>
    /// <response code="409">Email has been already verified</response>
    [HttpPut("{email}/verify")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<NoContentResult> PatchVerifyAsync(string email, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PatchVerificationRequest(email), cancellationToken);
      return NoContent();
    }


    /// <summary>
    /// Verifies account connected with email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="emailToken"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="302">Email has been verified</response>
    /// <response code="400">Email is invalid or token is invalid</response>
    /// <response code="404">Email not found</response>
    /// <response code="409">Email has been already verified</response>
    [HttpGet("{email}/verify")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<RedirectResult> GetVerifyAsync(string email, [FromQuery] string emailToken, CancellationToken cancellationToken)
    {
      var model = await fMediator.Send(new GetVerificationRequest(email: email, emailToken: emailToken), cancellationToken);
      await fMediator.Publish(new UserVerificationStatusChangedNotification(model.Id, true), cancellationToken);

      return Redirect($"{EnvironmentUtils.PortalUrl}/emailConfirmed");
    }

    /// <summary>
    /// Enables password reset for user with given email and sends email with password reset token.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Reset enabled and token sent</response>
    /// <response code="400">Request not valid</response>
    [HttpPatch("{email}/passwordReset")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<NoContentResult> PatchPasswordResetAsync(string email, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PatchPasswordResetRequest(email), cancellationToken);
      return NoContent();
    }
  }
}
