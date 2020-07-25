﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Authorization;
using Shamyr.Cloud.Gateway.Service.Models.Users;
using Shamyr.Cloud.Gateway.Service.Notifications.Users;
using Shamyr.Cloud.Gateway.Service.Requests.Users;

namespace Shamyr.Cloud.Gateway.Service.Controllers.V1
{
  /// <summary>
  /// Controller for manipulating with user accounts
  /// </summary>
  [ApiController]
  [Route("api/v1/users", Name = "Users")]
  public class UsersController: ControllerBase
  {
    private const string _GetUserRoute = "GetUser";

    private readonly IMediator fMediator;

    public UsersController(IMediator mediator)
    {
      fMediator = mediator;
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
    [ProducesResponseType(typeof(UserPreviewsModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<UserPreviewsModel> GetManyAsync([FromQuery] UserQueryFilter filter, [FromQuery] UserSortModel sort, CancellationToken cancellationToken)
    {
      return await fMediator.Send(new GetManyRequest(filter, sort), cancellationToken);
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
    [ProducesResponseType(typeof(UserDetailModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<UserDetailModel> GetAsync([FromRoute] ObjectId id, CancellationToken cancellationToken)
    {
      return await fMediator.Send(new GetRequest(id), cancellationToken);
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PostAsync([FromBody] UserPostModel model, CancellationToken cancellationToken)
    {
      var idModel = await fMediator.Send(new PostRequest(model), cancellationToken);
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
    public async Task<IActionResult> PatchPasswordResetAsync([FromRoute] ObjectId id, [FromBody] UserPatchPasswordModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PatchPasswordResetRequest(id, model), cancellationToken);
      await fMediator.Send(new DeleteLoginRequest(id), cancellationToken);
      await fMediator.Publish(new UserLoggedOutNotification(id), cancellationToken);

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
    public async Task<IActionResult> PutDisabledAsync([FromRoute] ObjectId id, [FromBody] UserPutDisabledModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutDisabledRequest(id, model), cancellationToken);
      await fMediator.Publish(new UserDisabledStatusChangedNotification(id, model.Disabled), cancellationToken);

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
    public async Task<IActionResult> PutAdminAsync([FromRoute] ObjectId id, [FromBody] UserPutAdminModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutAdminRequest(id, model), cancellationToken);
      return NoContent();
    }
  }
}
