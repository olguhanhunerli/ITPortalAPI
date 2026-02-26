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
    public class BusinessHourRuleRepository : GenericRepository<BusinessHoursRule, ulong>, IBusinessHourRuleRepository
    {
        public BusinessHourRuleRepository(AppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
        }

        public async Task DeleteByBusinessHoursIdAsync(ulong businessHoursId)
        {
            var entities = await _set
               .Where(x => x.BusinessHoursId == businessHoursId)
               .ToListAsync();

            _set.RemoveRange(entities);

            await _context.SaveChangesAsync();
        }

        public async Task<List<BusinessHoursRule>> GetByBusinessHoursIdAsync(ulong businessHoursId)
        {
            return await _set
                .AsNoTracking()
                .Include(x => x.BusinessHours)
                .ToListAsync();
        }
    }
}
