using Microsoft.AspNetCore.Mvc;
using TicketPunch.Api.Resources;

namespace TicketPunch.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    class LicenseController : ControllerBase {
        public ActionResult<TicketResource> GetAuthorizationByUserName(string username) {
            return null;
        }
    }
}