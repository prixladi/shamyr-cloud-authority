using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Service.Extensions;
using Shamyr.Cloud.Authority.Service.Models.Users;
using Shamyr.Cloud.Authority.Service.Requests.CurrentUser;
using Shamyr.Cloud.Authority.Service.Services.Identity;

namespace Shamyr.Cloud.Authority.Service.Handlers.Requests.CurrentUser
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
      return Task.FromResult(identity.ToDetail());
    }
  }
}
