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

        public async Task<string> GenerateTicketNumberAsync(ulong ticketId)
        {
            return $"TCK-{ticketId:D6}";
        }

        public async Task<Ticket?> GetByTicketIdAsync(ulong id)
        {
            var q = _set
             .Where(t => t.Id == id)
            .Include(x => x.Type)
            .Include(x => x.Status)
            .Include(x => x.Priority)
            .Include(x => x.Impact)
            .Include(x => x.Urgency)
            .Include(x => x.ApprovalState)

            .Include(x => x.Category)
            .Include(x => x.Subcategory)

            .Include(x => x.Service)
            .Include(x => x.ConfigurationItem)
            .Include(x => x.Requester)
            .Include(x => x.RequestedFor)
            .Include(x => x.Assignee)
            .Include(x => x.AssignedTeam)

            .Include(x => x.Department)
            .Include(x => x.Location)

            .Include(x => x.Comments)
            .Include(x => x.Events);

            return await q.SingleOrDefaultAsync();

        }

        public async Task<Ticket?> GetMyTicketByIdAsync(ulong userId, ulong ticketId)
        {
            return await _set
             .Where(t => t.Id == ticketId && (t.RequesterId == userId || t.RequestedForId == userId))
             .Include(x => x.Type)
             .Include(x => x.Status)
             .Include(x => x.Priority)
             .Include(x => x.Impact)
             .Include(x => x.Urgency)
             .Include(x => x.ApprovalState)

             .Include(x => x.Category)
             .Include(x => x.Subcategory)

            .Include(x => x.Service)
            .Include(x => x.ConfigurationItem)

             .Include(x => x.Requester)
             .Include(x => x.RequestedFor)
             .Include(x => x.Assignee)
             .Include(x => x.AssignedTeam)

             .Include(x => x.Department)
             .Include(x => x.Location)

             .Include(x => x.Comments)
             .Include(x => x.Events)
             .SingleOrDefaultAsync();

           
        }

        public async Task<PagedResultDTO<Ticket>> GetMyTicketsPageAsync(ulong userId, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            pageSize = Math.Min(pageSize, 100);

            var query = _set
                .AsNoTracking()
                .Where(t => t.RequesterId == userId || t.RequestedForId == userId)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.Requester)
                .Include(t => t.Service)
                .Include(x => x.ConfigurationItem)

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
                .Include(t => t.Service)
                .Include(x => x.ConfigurationItem)

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
