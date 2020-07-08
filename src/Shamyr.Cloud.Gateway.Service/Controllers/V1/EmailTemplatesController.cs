using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Shamyr.Cloud.Gateway.Service.Authorization;
using Shamyr.Cloud.Gateway.Service.Models.EmailTemplates;
using Shamyr.Cloud.Gateway.Service.Requests.EmailTemplates;

namespace Shamyr.Cloud.Gateway.Service.Controllers.V1
{
  /// <summary>
  /// Controller for manipulating email templates
  /// </summary>
  [Route("api/v1/emailTemplates", Name = "Email templates")]
  public class EmailTemplatesController: Controller
  {
    private const string _GetEmailTemplateRoute = "GetEmailTemplate";

    private readonly IMediator fMediator;

    public EmailTemplatesController(IMediator mediator)
    {
      fMediator = mediator;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PostAsync([FromBody] EmailTemplatePostModel model, CancellationToken cancellationToken)
    {
      var idModel = await fMediator.Send(new PostRequest(model), cancellationToken);
      return CreatedAtRoute(_GetEmailTemplateRoute, new { id = idModel.Id.ToString() }, idModel);
    }

    /// <summary>
    /// Gets all clients.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Returns list of clients</response>
    /// <response code="403">Insufficient permission</response>
    /// <response code="404">Email template with given id not found</response>
    [HttpGet]
    [Authorize(UserPolicy._Admin)]
    [ProducesResponseType(typeof(ICollection<EmailTemplatePreviewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ICollection<EmailTemplatePreviewModel>> GetManyAsync(CancellationToken cancellationToken)
    {
      return await fMediator.Send(new GetManyRequest(), cancellationToken);
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
    [ProducesResponseType(typeof(EmailTemplatePreviewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<EmailTemplateDetailModel> GetAsync([FromRoute] ObjectId id, CancellationToken cancellationToken)
    {
      return await fMediator.Send(new GetRequest(id), cancellationToken);
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
    public async Task<IActionResult> PutAsync([FromRoute] ObjectId id, [FromBody] EmailTemplatePutModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PutRequest(id, model), cancellationToken);
      return NoContent();
    }

    /// <summary>
    /// Applies patch on email template document.
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
    public async Task<IActionResult> PutNameAsync([FromRoute] ObjectId id, [FromBody] EmailTemplatePutNameModel model, CancellationToken cancellationToken)
    {
      await fMediator.Send(new PatchNameRequest(id, model), cancellationToken);
      return NoContent();
    }

    private static IRequest ResolvePatch
  }
}
