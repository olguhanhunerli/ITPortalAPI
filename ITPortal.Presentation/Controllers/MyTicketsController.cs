using ITPortal.Entities.DTOs.TicketDTOs;
using ITPortal.Presentation.Authorization;
using ITPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITPortal.Presentation.Controllers
{
    [Route("api/portal/my-tickets")]
    [Authorize(Roles = RoleGroups.TicketCreate)]
    public class MyTicketsController : BaseApiController
    {
        private readonly ITicketService _ticketService;

        public MyTicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpGet]
        [Authorize(Roles = RoleGroups.PortalUsers)]
        public async Task<IActionResult> GetMyTicketsPage(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _ticketService.GetMyTicketsPageAsync(CurrentUserId.Value,pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = RoleGroups.PortalUsers)]
        public async Task<IActionResult> GetMyTicketsById(ulong id)
        {
            var result = await _ticketService.GetMyTicketByIdAsync(CurrentUserId.Value,id);
            if (result == null)
                return NotFoundMsg("Ticket Bulunamadı");
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = RoleGroups.PortalUsers)]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDTO request)
        {
            EnsureAuthenticated();
            var result = await _ticketService.CreateTicketAsync(request, CurrentUserId!.Value);
            return Ok(result);
        }
    }
}