using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.TicketDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ITPortal.Business.Repository
{
    public class TicketRepository : GenericRepository<Ticket, ulong>, ITicketRepository
    {
        public TicketRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public async Task<PagedResultDTO<Ticket>> GetTicketsPageAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            pageSize = Math.Min(pageSize, 100);

            var query = _set
                .AsNoTracking()
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.Requester)
                .Include(t => t.Assignee);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDTO<Ticket>
            {
                Page = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = items
            };

        }
    }
}
