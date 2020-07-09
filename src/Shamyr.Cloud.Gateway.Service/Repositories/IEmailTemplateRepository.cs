using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Dtos.EmailTemplates;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Gateway.Service.Repositories
{
  public interface IEmailTemplateRepository: IRepositoryBase<EmailTemplateDoc>
  {
    Task<EmailTemplateDoc?> GetByTypeAsync(EmailTemplateType type, CancellationToken cancellationToken);
    Task<bool> ExistsByTypeAsync(EmailTemplateType type, CancellationToken cancellationToken);
    Task<List<EmailTemplatePreviewDto>> GetAsync(CancellationToken cancellationToken);
    Task<bool> DeleteAsync(ObjectId id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ObjectId id, EmailTemplateUpdateDto updateDto, CancellationToken cancellationToken);
    Task<bool> UpdatePropAsync<T>(ObjectId id, Expression<Func<EmailTemplateDoc, T>> selector, T value, CancellationToken cancellationToken);
  }
}