﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Repositories;
using Shamyr.Cloud.Gateway.Service.Requests.EmailTemplates;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.EmailTemplates
{
  public class PatchSubjectRequestHandler: IRequestHandler<PatchSubjectRequest>
  {
    private readonly IEmailTemplateRepository fTemplateRepository;

    public PatchSubjectRequestHandler(IEmailTemplateRepository templateRepository)
    {
      fTemplateRepository = templateRepository;
    }

    public async Task<Unit> Handle(PatchSubjectRequest request, CancellationToken cancellationToken)
    {
      if (!await fTemplateRepository.UpdatePropAsync(request.TemplateId, doc => doc.Subject, request.Subject, cancellationToken))
        throw new NotFoundException($"Email template with ID {request.TemplateId} does not exist.");

      return Unit.Value;
    }
  }
}
