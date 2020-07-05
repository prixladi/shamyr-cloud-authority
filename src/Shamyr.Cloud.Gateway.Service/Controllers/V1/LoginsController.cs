using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shamyr.Cloud.Gateway.Service.Models.Logins;
using Shamyr.Cloud.Gateway.Service.Notifications.CurrentUser;
using Shamyr.Cloud.Gateway.Service.Requests.Logins;

namespace Shamyr.Cloud.Gateway.Service.Controllers.V1
{
  /// <summary>
  /// Controller for logins
  /// </summary>
  [Route("api/v1/logins", Name = "Logins")]
  public class LoginsController: Controller
  {
    private readonly IMediator fMediator;

    public LoginsController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Logs user in
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">UserDoc logged in</response>
    /// <response code="400">Model is invalid, username is invalid or password is invalid</response>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TokensModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<TokensModel> PostAsync([FromBody] LoginPostModel model, CancellationToken cancellationToken)
    {
      return fMediator.Send(new PostRequest(model), cancellationToken);
    }

    /// <summary>
    /// Logs user out
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="204">UserDoc logged out</response>
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<NoContentResult> DeleteAsync(CancellationToken cancellationToken)
    {
      await fMediator.Send(new DeleteRequest { }, cancellationToken);
      await fMediator.Publish(new CurrentUserLoggedOutNotification(), cancellationToken);

      return NoContent();
    }

    /// <summary>
    /// Refreshes user token
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">UserDoc login refreshed</response>
    /// <response code="400">Model is invalid, refresh token is invalid.</response>
    [HttpPut("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TokensModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<TokensModel> PutRefreshAsync([FromBody] LoginPutRefreshModel model, CancellationToken cancellationToken)
    {
      return fMediator.Send(new PutRefreshRequest(model), cancellationToken);
    }
  }
}
