using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Shamyr.Cloud.Identity.Service.Models;
using Shamyr.Cloud.Identity.Service.Requests.Users;

namespace Shamyr.Cloud.Identity.Service.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/v1/users")]
  public class UsersController: ControllerBase
  {
    private readonly IMediator fMediator;

    public UsersController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Returns user with provided ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<UserModel> GetAsync([FromRoute] ObjectId id, CancellationToken cancellationToken)
    {
      return await fMediator.Send(new GetRequest(id), cancellationToken);
    }
  }
}
