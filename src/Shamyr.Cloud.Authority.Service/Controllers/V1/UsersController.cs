﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Authorization;
using Shamyr.Cloud.Authority.Service.Models;
using Shamyr.Cloud.Authority.Service.Models.Users;
using Shamyr.Cloud.Authority.Service.Notifications.Users;
using Shamyr.Cloud.Authority.Service.Requests.Users;

namespace Shamyr.Cloud.Authority.Service.Controllers.V1
{
  /// <summary>
  /// Controller for manipulating with user accounts
  /// </summary>
  [ApiController]
  [Route("api/v1/users", Name = "Users")]
  public class UsersController: ControllerBase
  {
    private const string _GetUserRoute = "GetUser";

    private readonly ISender fSender;
    private readonly IPublisher fPublisher;

    public UsersController(ISender sender, IPublisher publisher)
    {
      fSender = sender;
      fPublisher = publisher;
    }

    /// <summary>
    /// Gets users.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="sort"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Model with list of users</response>
    /// <response code="400">Parameters are not valid</response>
    /// <response code="403">Insufficient permission.</response>
    [HttpGet]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(typeof(PreviewsModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<PreviewsModel> GetManyAsync([FromQuery] QueryFilter filter, [FromQuery] SortModel sort, CancellationToken cancellationToken)
    {
      return await fSender.Send(new GetManyRequest(filter, sort), cancellationToken);
    }

    /// <summary>
    /// Gets user's detail.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">List of users</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="403">Insufficient permission</response>
    /// <response code="404">User with givent id not found</response>
    [HttpGet("{id}", Name = _GetUserRoute)]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(typeof(DetailModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DetailModel> GetAsync([FromRoute] ObjectId id, CancellationToken cancellationToken)
    {
      return await fSender.Send(new GetRequest(id), cancellationToken);
    }

    /// <summary>
    /// Creates new user.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="201">User created</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="409">Email or username already occupied</response>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IdModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PostAsync([FromBody] PostModel model, CancellationToken cancellationToken)
    {
      var idModel = await fSender.Send(new PostRequest(model), cancellationToken);
      return CreatedAtRoute(_GetUserRoute, new { id = idModel.Id.ToString() }, idModel);
    }

    /// <summary>
    /// Sets new password and logs user out.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">New password has been set and user has been logged out</response>
    /// <response code="400">Invalid model or token</response>
    /// <response code="404">User not found</response>
    /// <response code="409">No password reset requested</response>
    [HttpPatch("{id}/passwordReset")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PatchPasswordResetAsync([FromRoute] ObjectId id, [FromBody] PatchPasswordModel model, CancellationToken cancellationToken)
    {
      await fSender.Send(new PatchPasswordResetRequest(id, model), cancellationToken);
      await fSender.Send(new DeleteLoginRequest(id), cancellationToken);
      await fPublisher.Publish(new LoggedOutNotification(id), cancellationToken);

      return NoContent();
    }

    /// <summary>
    /// Disables or enables user.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">User disabled</response>
    /// <response code="400">Request not valid</response>
    /// <response code="403">Insufficient permission.</response>
    /// <response code="404">User not found</response>
    [HttpPut("{id}/disabled")]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutDisabledAsync([FromRoute] ObjectId id, [FromBody] PutDisabledModel model, CancellationToken cancellationToken)
    {
      await fSender.Send(new PutDisabledRequest(id, model), cancellationToken);
      await fSender.Send(new DeleteLoginRequest(id), cancellationToken);

      await Task.WhenAll
      (
        fPublisher.Publish(new LoggedOutNotification(id), cancellationToken),
        fPublisher.Publish(new DisabledChangedNotification(id, model.Disabled), cancellationToken)
      );

      return NoContent();
    }

    /// <summary>
    /// Changes user admin status.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">User admin status changed</response>
    /// <response code="400">Request not valid</response>
    /// <response code="403">Insufficient permission.</response>
    /// <response code="404">User not found</response>
    [HttpPut("{id}/admin")]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAdminAsync([FromRoute] ObjectId id, [FromBody] PutAdminModel model, CancellationToken cancellationToken)
    {
      await fSender.Send(new PutAdminRequest(id, model), cancellationToken);
      await fSender.Send(new DeleteLoginRequest(id), cancellationToken);

      await Task.WhenAll
      (
        fPublisher.Publish(new LoggedOutNotification(id), cancellationToken),
        fPublisher.Publish(new AdminChangedNotification(id, model.Admin), cancellationToken)
      );

      return NoContent();
    }
  }
}
