using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface IBusinessHourRuleRepository : IGenericRepository<BusinessHoursRule, ulong>
    {
        Task<List<BusinessHoursRule>> GetByBusinessHoursIdAsync(ulong businessHoursId);
        Task DeleteByBusinessHoursIdAsync(ulong businessHoursId);
    }
}
