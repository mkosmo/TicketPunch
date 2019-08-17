using Microsoft.AspNetCore.Mvc;
using TicketPunch.Api.Resources;
using TicketPunch.Core.Services;

namespace TicketPunch.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    class LicenseController : ControllerBase {
        
        private ITicketIssuingService _ticketIssuingService;

        LicenseController(ITicketIssuingService ticketIssuingService) {
            _ticketIssuingService = ticketIssuingService;
        }

        public ActionResult<TicketResource> GetAuthorizationByUserName(string username) {
            return null;
        }
    }
}