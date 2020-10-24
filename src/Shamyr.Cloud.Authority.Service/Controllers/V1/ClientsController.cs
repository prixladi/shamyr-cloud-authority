using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Authorization;
using Shamyr.Cloud.Authority.Service.Models.Clients;
using Shamyr.Cloud.Authority.Service.Requests.Clients;

namespace Shamyr.Cloud.Authority.Service.Controllers.V1
{
  /// <summary>
  /// Controller for manipulating clients
  /// </summary>
  [ApiController]
  [Route("api/v1/clients", Name = "Clients")]
  public class ClientsController: ControllerBase
  {
    private const string _GetClientRoute = "GetClient";

    private readonly IMediator fMediator;

    public ClientsController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Creates new client.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="201">Client created</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="403">Insufficient permission</response>
    /// <response code="409">Name already occupied</response>
    [HttpPost]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PostAsync([FromBody] PostModel model, CancellationToken cancellationToken)
    {
      var idModel = await fMediator.Send(new PostRequest(model), cancellationToken);
      return CreatedAtRoute(_GetClientRoute, new { id = idModel.Id.ToString() }, idModel);
    }

    /// <summary>
    /// Gets all clients.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Returns list of clients</response>
    /// <response code="403">Insufficient permission</response>
    /// <response code="404">Client with given id not found</response>
    [HttpGet]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(typeof(ICollection<PreviewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ICollection<PreviewModel>> GetManyAsync(CancellationToken cancellationToken)
    {
      return await fMediator.Send(new GetManyRequest(), cancellationToken);
    }

    /// <summary>
    /// Gets client.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Returns client</response>
    /// <response code="403">Insufficient permission</response>
    /// <response code="404">Client with given id not found</response>
    [HttpGet("{id}", Name = _GetClientRoute)]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(typeof(PreviewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DetailModel> GetAsync([FromRoute] ObjectId id, CancellationToken cancellationToken)
    {
      return await fMediator.Send(new GetRequest(id), cancellationToken);
    }

    /// <summary>
    /// Changes existing client.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Client changed</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="403">Insufficient permission</response>
    /// <response code="404">Client with given id not found</response>
    /// <response code="409">Name already occupied</response>
    [HttpPut("{id}")]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PutAsync([FromRoute] ObjectId id, [FromBody] PutModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutRequest(id, model), cancellationToken);
      return NoContent();
    }

    /// <summary>
    /// Changes existing client.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Client disabled status changed</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="403">Insufficient permission</response>
    /// <response code="404">Client with given id not found</response>
    [HttpPut("{id}/disabled")]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutDisabledAsync([FromRoute] ObjectId id, [FromBody] PutDisabledModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutDisabledRequest(id, model), cancellationToken);
      return NoContent();
    }
  }
}
