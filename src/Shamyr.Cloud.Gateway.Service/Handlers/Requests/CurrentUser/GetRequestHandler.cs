using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Gateway.Service.Extensions.Models;
using Shamyr.Cloud.Gateway.Service.Models.Users;
using Shamyr.Cloud.Gateway.Service.Requests.CurrentUser;
using Shamyr.Cloud.Gateway.Service.Services.Identity;

namespace Shamyr.Cloud.Gateway.Service.Handlers.Requests.CurrentUser
{
  public class GetRequestHandler: IRequestHandler<GetRequest, UserDetailModel>
  {
    private readonly IIdentityService fIdentityService;

    public GetRequestHandler(IIdentityService identityService)
    {
      fIdentityService = identityService;
    }

    public Task<UserDetailModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
      var identity = fIdentityService.Current;

      return Task.FromResult(identity.ToModel());
    }
  }
}
