using ITPortal.Entities.DTOs.TicketDTOs;
using ITPortal.Presentation.Authorization;
using ITPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Presentation.Controllers
{
    [Route("api/admin/tickets")]
    [Authorize(Roles = RoleGroups.TicketReadWrite)]
    public class AdminTicketsController : BaseApiController
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketEventService _ticketEventService;

        public AdminTicketsController(ITicketService ticketService, ITicketEventService ticketEventService)
        {
            _ticketService = ticketService;
            _ticketEventService = ticketEventService;
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
    }
}
