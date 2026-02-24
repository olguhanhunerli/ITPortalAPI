using ITPortal.Business.Context;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository
{
    public class TicketCommentRepository : GenericRepository<TicketComment, ulong>, ITicketCommentRepository
    {
        public TicketCommentRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public async Task<TicketComment?> GetDetailByIdAsync(ulong id)
        {
            return await _set
                .Include(x => x.Ticket)
                .Include(x => x.Author)
                .Include(x => x.Visibility)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
