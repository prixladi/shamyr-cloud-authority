using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Authorization;
using Shamyr.Cloud.Gateway.Service.Models.UserPermissions;
using Shamyr.Cloud.Gateway.Service.Notifications.Users;
using Shamyr.Cloud.Gateway.Service.Requests.UserPermissions;
using Shamyr.Cloud.Gateway.Signal.Messages;

namespace Shamyr.Cloud.Gateway.Service.Controllers.V1
{
  /// <summary>
  /// Controller for manipulating with user permissions
  /// </summary>
  [Route("api/v1/users/{userId}/permission", Name = "User permission")]
  public class UserPermissionsController: Controller
  {
    private readonly IMediator fMediator;

    public UserPermissionsController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Get user permission.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Returns user permission</response>
    /// <response code="403">Insufficient permission, permission needed '<see cref="PermissionKind.Control"/>'</response>
    /// <response code="404">UserDoc not found</response>
    [HttpGet]
    [Authorize(UserPolicy._View)]
    [ProducesResponseType(typeof(PermissionDetailModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<PermissionDetailModel> GetAsync(ObjectId userId, CancellationToken cancellationToken)
    {
      return fMediator.Send(new GetRequest(userId), cancellationToken);
    }

    /// <summary>
    /// Updates user permission.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">UserDoc permission updated</response>
    /// <response code="403">Insufficient permission, permission needed '<see cref="PermissionKind.Configure"/>'</response>
    /// <response code="404">UserDoc not found</response>
    [HttpPut]
    [Authorize(UserPolicy._Configure)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<NoContentResult> PutAsync(ObjectId userId, [FromBody] PermissionPatchModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutRequest(userId, model), cancellationToken);
      await fMediator.Publish(new UserUserPermissionChangedNotification(userId, model.Kind), cancellationToken);

      return NoContent();
    }
  }
}
