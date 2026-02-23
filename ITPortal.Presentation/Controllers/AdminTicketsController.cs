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

        public AdminTicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTicketsPage(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _ticketService.GetTicketsPageAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // Admin detail: herhangi bir ticket detayı
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

        //[HttpPut("{id:ulong}")]
        //public async Task<IActionResult> UpdateTicket(ulong id, [FromBody] UpdateTicketAdminDTO request)
        //{
        //    EnsureAuthenticated();

        //    var result = await _ticketService.UpdateTicketAdminAsync(
        //        id,
        //        request,
        //        CurrentUserId!.Value
        //    );

        //    if (result == null)
        //        return NotFoundMsg("Ticket Bulunamadı");

        //    return Ok(result);
        //}

        //[HttpPut("{id:ulong}/assign")]
        //public async Task<IActionResult> AssignTicket(ulong id, [FromBody] AssignTicketDTO request)
        //{
        //    EnsureAuthenticated();

        //    var result = await _ticketService.AssignTicketAsync(
        //        id,
        //        request,
        //        CurrentUserId!.Value
        //    );

        //    if (result == null)
        //        return NotFoundMsg("Ticket Bulunamadı");

        //    return Ok(result);
        //}
    }
}
