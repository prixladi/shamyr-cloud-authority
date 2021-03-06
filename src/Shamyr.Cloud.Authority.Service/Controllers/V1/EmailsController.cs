﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shamyr.Cloud.Authority.Service.Models;
using Shamyr.Cloud.Authority.Service.Models.Emails;
using Shamyr.Cloud.Authority.Service.Notifications.Users;
using Shamyr.Cloud.Authority.Service.Requests.Emails;

namespace Shamyr.Cloud.Authority.Service.Controllers.V1
{
  /// <summary>
  /// Controller for manupullating with emails
  /// </summary>
  [AllowAnonymous]
  [ApiController]
  [Route("api/v1/emails", Name = "Emails")]
  public class EmailsController: ControllerBase
  {
    private readonly ISender fSender;
    private readonly IPublisher fPublisher;

    public EmailsController(ISender sender, IPublisher publisher)
    {
      fSender = sender;
      fPublisher = publisher;
    }

    /// <summary>
    /// Sends email with account verification link
    /// </summary>
    /// <param name="email"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Verification email has been sent</response>
    /// <response code="400">Email is invalid</response>
    /// <response code="404">Email not found</response>
    /// <response code="409">Email has been already verified</response>
    [HttpPatch("{email}/verification")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<NoContentResult> PatchVerifyAsync([FromRoute] string email, [FromBody] ClientIdModel model, CancellationToken cancellationToken)
    {
      await fSender.Send(new PatchVerificationRequest(email, model), cancellationToken);
      return NoContent();
    }

    /// <summary>
    /// Verifies account connected with email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Email has been verified</response>
    /// <response code="400">Email is invalid or token is invalid</response>
    /// <response code="404">Email not found</response>
    /// <response code="409">Email has been already verified</response>
    [HttpPut("{email}/verified")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<NoContentResult> GetVerifyAsync([FromRoute] string email, [FromBody] TokenModel model, CancellationToken cancellationToken)
    {
      var result = await fSender.Send(new PutVerifiedRequest(email: email, emailToken: model.Token), cancellationToken);
      await fPublisher.Publish(new VerificationChangedNotification(result.Id, true), cancellationToken);
      return NoContent();
    }

    /// <summary>
    /// Enables password reset for user with given email and sends email with password reset token.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Reset enabled and token sent</response>
    /// <response code="400">Request not valid</response>
    [HttpPatch("{email}/passwordReset")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<NoContentResult> PatchPasswordResetAsync([FromRoute] string email, [FromBody] ClientIdModel model, CancellationToken cancellationToken)
    {
      await fSender.Send(new PatchPasswordResetRequest(email, model), cancellationToken);
      return NoContent();
    }
  }
}
