using ITPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITPortal.Presentation.Controllers
{
    public class TicketController : BaseApiController
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
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
    }
}