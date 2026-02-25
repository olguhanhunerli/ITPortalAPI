using ITPortal.Entities.DTOs.TicketAttachmentDTOs;
using ITPortal.Entities.DTOs.TicketDTOs;
using ITPortal.Presentation.Authorization;
using ITPortal.Services;
using ITPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITPortal.Presentation.Controllers
{
    [Route("api/admin/tickets")]
    [Authorize(Roles = RoleGroups.TicketReadWrite)]
    public class AdminTicketsController : BaseApiController
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketEventService _ticketEventService;
        private readonly ITicketAssignmentHistoryService _ticketAssignmentHistoryService;
        private readonly ITicketAttachmentService _ticketAttachmentService;
        public AdminTicketsController(ITicketService ticketService, ITicketEventService ticketEventService, ITicketAssignmentHistoryService ticketAssignmentHistoryService, ITicketAttachmentService ticketAttachmentService)
        {
            _ticketService = ticketService;
            _ticketEventService = ticketEventService;
            _ticketAssignmentHistoryService = ticketAssignmentHistoryService;
            _ticketAttachmentService = ticketAttachmentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTicketsPage(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _ticketService.GetTicketsPageAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(ulong id)
        {
            var result = await _ticketService.GetTicketByIdAsync(id);
            if (result == null)
                return NotFoundMsg("Ticket Bulunamadı");
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = RoleGroups.TicketCreate)]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDTO request)
        {
            EnsureAuthenticated();
            var result = await _ticketService.CreateTicketAsync(request, CurrentUserId!.Value);
            return Ok(result);
        }
        [HttpPut("{id}/assigne")]
        public async Task<IActionResult> AssignTicket(ulong id, [FromBody] UpdateTicketAssignmentDTO request)
        {
            var result = await _ticketService.UpdateTicketAssignment(id, request, CurrentUserId!.Value);
            return Ok(result);
        }
        [HttpGet("events")]
        public async Task<IActionResult> GetTicketEvents(int page = 1, int pageSize = 10)
        {
            var result = await _ticketEventService.GetTicketEvent(page, pageSize);
            return Ok(result);
        }
        [HttpGet("{ticketId}/events")]
        public async Task<IActionResult> GetTicketEventsById(ulong ticketId)
        {
            var result = await _ticketEventService.GetTicketEventById(ticketId);
            return Ok(result);
        }
        [HttpGet("assignment-history")]
        public async Task<IActionResult> GetTicketAssignmentHistory(int page = 1, int pageSize = 10)
        {
            var result = await _ticketAssignmentHistoryService.GetTicketAssignmentHistoryAllAsync(page, pageSize);
            return Ok(result);
        }
        [HttpGet("{ticketId}/assignment-history")]
        public async Task<IActionResult> GetTicketAssignmentHistoryByTicketId(ulong ticketId)
        {
            var result = await _ticketAssignmentHistoryService.GetTicketAssignmentHistoryByTicketIdAsync(ticketId);
            return Ok(result);
        }
        [HttpPost("{ticketId}/attachments")]
        public async Task<IActionResult> Upload(ulong ticketId, [FromForm] CreateTicketAttachmentDTO dto)
        {
            var result = await _ticketAttachmentService.CreateTicketAttachmentAsyn(
                ticketId, dto, CurrentUserId!.Value);

            return Ok(result);
        }
    }
}
