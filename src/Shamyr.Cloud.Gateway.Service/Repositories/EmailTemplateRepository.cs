using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shamyr.Cloud.Database.Documents;
using Shamyr.Cloud.Gateway.Service.Dtos.EmailTemplates;
using Shamyr.Cloud.Gateway.Service.Expressions;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;

namespace Shamyr.Cloud.Gateway.Service.Repositories
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

    public async Task<List<EmailTemplatePreviewDto>> GetAsync(CancellationToken cancellationToken)
    {
      return await Query
        .Select(EmailTemplateDocExpression.ToPreviewDto)
        .ToListAsync(cancellationToken);
    }

    public async Task<EmailTemplateDoc?> GetByTypeAsync(EmailTemplateType type, CancellationToken cancellationToken)
    {
      return await Query.SingleOrDefaultAsync(doc => doc.Type == type, cancellationToken);
    }

    public async Task<bool> ExistsByTypeAsync(EmailTemplateType type, CancellationToken cancellationToken)
    {
      return await Query.AnyAsync(doc => doc.Type == type, cancellationToken);
    }

    public async Task<bool> UpdateAsync(ObjectId id, EmailTemplateUpdateDto updateDto, CancellationToken cancellationToken)
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

    public async Task<bool> UpdatePropAsync<T>(ObjectId id, Expression<Func<EmailTemplateDoc, T>> selector, T value, CancellationToken cancellationToken)
    {
      var update = Builders<EmailTemplateDoc>.Update
        .Set(selector, value);

      var result = await UpdateAsync(id, update, cancellationToken);
      return result.MatchedCount == 1;
    }
  }
}
