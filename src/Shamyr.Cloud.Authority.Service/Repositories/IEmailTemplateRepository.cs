using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Service.Dtos.EmailTemplates;
using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Authority.Service.Repositories
{
  public interface IEmailTemplateRepository: IRepositoryBase<EmailTemplateDoc>
  {
    Task<List<PreviewDto>> GetAsync(FilterDto filter, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ObjectId id, UpdateDto updateDto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ObjectId id, UpdateWithBodyDto updateDto, CancellationToken cancellationToken);
  }
}