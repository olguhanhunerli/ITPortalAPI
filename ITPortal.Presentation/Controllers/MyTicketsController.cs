using ITPortal.Entities.DTOs.TicketCommentDTOs;
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
        private readonly ITicketCommentService _ticketCommentService;

        public MyTicketsController(ITicketService ticketService, ITicketCommentService ticketCommentService)
        {
            _ticketService = ticketService;
            _ticketCommentService = ticketCommentService;
        }
        [HttpGet]
        [Authorize(Roles = RoleGroups.PortalUsers)]
        public async Task<IActionResult> GetMyTicketsPage(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _ticketService.GetMyTicketsPageAsync(CurrentUserId.Value, pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = RoleGroups.PortalUsers)]
        public async Task<IActionResult> GetMyTicketsById(ulong id)
        {
            var result = await _ticketService.GetMyTicketByIdAsync(CurrentUserId.Value, id);
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
        [HttpPost("{ticketId}/comments")]
        [Authorize(Roles = RoleGroups.PortalUsers)]
        public async Task<IActionResult> AddCommentToTicket(ulong ticketId, [FromBody] CreateCommentDTO request)
        {
            var result = await _ticketCommentService.CreateTicketCommentAsync(ticketId, request, CurrentUserId!.Value);
            return Ok(result);
        }
        [HttpPut]
        [Route("{ticketId}/complete")]
        [Authorize(Roles = RoleGroups.PortalUsers)]
        public async Task<IActionResult> CompleteTicket(ulong ticketId, [FromForm] UpdateStatuTicketDTO dto)
        {
            var result = await _ticketService.ComplateTicketByIdAsync(ticketId, CurrentUserId!.Value, dto);
            return Ok(result);
        }
        [HttpPut]
        [Route("{ticketId}/reopen")]
        [Authorize(Roles = RoleGroups.PortalUsers)]
        public async Task<IActionResult> ReopenTicket(ulong ticketId, [FromForm] UpdateStatuTicketDTO dto)
        {
            var result = await _ticketService.ReopenTicketByIdAsync(ticketId, CurrentUserId!.Value, dto);
            return Ok(result);
        }
    }
}