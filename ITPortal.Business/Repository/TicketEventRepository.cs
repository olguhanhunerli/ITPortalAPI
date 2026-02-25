using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ITPortal.Business.Repository
{
    public class TicketEventRepository : GenericRepository<TicketEvent, ulong>, ITicketEventRepository
    {
        public TicketEventRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {

        }

        public async Task<PagedResultDTO<TicketEvent>> GetTicketEventAsync(int pageNumber, int pageSize)
        {
            if(pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageSize > 100) pageSize = 100;

            var totalCount = await _set.CountAsync();

            var baseQuery = _set
                .AsNoTracking()
                .Include(x => x.Actor)
                .Include(x => x.Ticket)
                .OrderBy(l => l.Id);
            return new PagedResultDTO<TicketEvent>
            {
                TotalCount = totalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = await baseQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync()
            };
        }

        public Task<List<TicketEvent>> GetTicketEventByIdAsync(ulong ticketId)
        {
            return _set
                 .AsNoTracking()
                 .Where(x => x.TicketId == ticketId)
                 .Include(x => x.Actor)
                 .Include(x => x.Ticket)
                 .ToListAsync();
        }
    }
}
