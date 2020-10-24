using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shamyr.Cloud.Authority.Service.Dtos.EmailTemplates;
using Shamyr.Cloud.Authority.Service.Expressions;
using Shamyr.Cloud.Database.Documents;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Authority.Service.Repositories
{
  public class EmailTemplateRepository: RepositoryBase<EmailTemplateDoc>, IEmailTemplateRepository
  {
    public EmailTemplateRepository(IDatabaseContext dbContext)
      : base(dbContext) { }

    public async Task<bool> DeleteAsync(ObjectId id, CancellationToken cancellationToken)
    {
      var result = await Collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
      return result.DeletedCount == 1;
    }

    public async Task<List<PreviewDto>> GetAsync(FilterDto filter, CancellationToken cancellationToken)
    {
      return await Query
        .WhereType(filter.TemplateType)
        .Select(EmailTemplateDocExpression.ToPreviewDto)
        .ToListAsync(cancellationToken);
    }
    public async Task<bool> UpdateAsync(ObjectId id, UpdateWithBodyDto updateDto, CancellationToken cancellationToken)
    {
      var update = Builders<EmailTemplateDoc>.Update
        .Set(x => x.Subject, updateDto.Subject)
        .Set(x => x.Name, updateDto.Name)
        .Set(x => x.Body, updateDto.Body)
        .Set(x => x.IsHtml, updateDto.IsHtml)
        .Set(x => x.Type, updateDto.Type);

      var result = await UpdateAsync(id, update, cancellationToken);
      return result.MatchedCount == 1;
    }

    public async Task<bool> UpdateAsync(ObjectId id, UpdateDto updateDto, CancellationToken cancellationToken)
    {
      var update = Builders<EmailTemplateDoc>.Update
        .Set(x => x.Subject, updateDto.Subject)
        .Set(x => x.Name, updateDto.Name)
        .Set(x => x.IsHtml, updateDto.IsHtml)
        .Set(x => x.Type, updateDto.Type);

      var result = await UpdateAsync(id, update, cancellationToken);
      return result.MatchedCount == 1;
    }
  }
}
