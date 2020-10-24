using System;
using System.Collections.Generic;
using MediatR;
using Shamyr.Cloud.Authority.Service.Models.EmailTemplates;

namespace Shamyr.Cloud.Authority.Service.Requests.EmailTemplates
{
  public class GetManyRequest: IRequest<ICollection<PreviewModel>>
  {
    public QueryFilter Filter { get; }

    public GetManyRequest(QueryFilter filter)
    {
      Filter = filter ?? throw new ArgumentNullException(nameof(filter));
    }
  }
}
