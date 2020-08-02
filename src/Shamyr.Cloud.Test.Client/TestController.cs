using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shamyr.Cloud.Identity.Client.Test.Controllers
{
  [Authorize]
  [Route("api/v1/test")]
  public class TestController: ControllerBase
  {
    [HttpGet]
    public async Task GetAsync()
    {
      await Task.CompletedTask;
    }
  }
}
