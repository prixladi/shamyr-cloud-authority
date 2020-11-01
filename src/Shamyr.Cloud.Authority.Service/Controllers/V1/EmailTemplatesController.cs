using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Authorization;
using Shamyr.Cloud.Authority.Service.Models;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;
using Shamyr.Cloud.Authority.Service.Requests.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Controllers.V1
{
  /// <summary>
  /// Controller for manipulating email templates
  /// </summary>
  [ApiController]
  [Route("api/v1/emailTemplates", Name = "Email templates")]
  public class EmailTemplatesController: ControllerBase
  {
    private const string _GetEmailTemplateRoute = "GetEmailTemplate";

    private readonly ISender fSender;

    public EmailTemplatesController(ISender sender)
    {
      fSender = sender;
    }

    /// <summary>
    /// Creates new email template.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="201">Email template created</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="403">Insufficient permission</response>
    [HttpPost]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(typeof(IdModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PostAsync([FromBody] PostModel model, CancellationToken cancellationToken)
    {
      var idModel = await fSender.Send(new PostRequest(model), cancellationToken);
      return CreatedAtRoute(_GetEmailTemplateRoute, new { id = idModel.Id.ToString() }, idModel);
    }

    /// <summary>
    /// Gets all email templates.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Returns list of clients</response>
    /// <response code="403">Insufficient permission</response>
    /// <response code="404">Email template with given id not found</response>
    [HttpGet]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(typeof(ICollection<PreviewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ICollection<PreviewModel>> GetManyAsync([FromQuery] QueryFilter filter, CancellationToken cancellationToken)
    {
      return await fSender.Send(new GetManyRequest(filter), cancellationToken);
    }

    /// <summary>
    /// Gets email template.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Returns email template</response>
    /// <response code="403">Insufficient permission</response>
    /// <response code="404">Email template with given id not found</response>
    [HttpGet("{id}", Name = _GetEmailTemplateRoute)]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(typeof(PreviewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DetailModel> GetAsync([FromRoute] ObjectId id, CancellationToken cancellationToken)
    {
      return await fSender.Send(new GetRequest(id), cancellationToken);
    }

    /// <summary>
    /// Changes existing email template.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Email template changed</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="403">Insufficient permission</response>
    /// <response code="404">Email template with given id not found</response>
    [HttpPut("{id}")]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAsync([FromRoute] ObjectId id, [FromBody] PutModel model, CancellationToken cancellationToken)
    {
      await fSender.Send(new PutRequest(id, model), cancellationToken);
      return NoContent();
    }

    /// <summary>
    /// Applies patch on email template.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Email template patched</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="403">Insufficient permission</response>
    /// <response code="404">Email template with given id not found</response>
    [HttpPatch("{id}")]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchAsync([FromRoute] ObjectId id, [FromBody] PatchModel model, CancellationToken cancellationToken)
    {
      await fSender.Send(new PatchRequest(id, model), cancellationToken);
      return NoContent();
    }
  }
}
