using MediatR;
using Shamyr.Cloud.Authority.Models;

namespace Shamyr.Cloud.Authority.Service.Requests.Token
{
  public class GetConfigurationRequest: IRequest<TokenConfigurationModel>
  {
  }
}
