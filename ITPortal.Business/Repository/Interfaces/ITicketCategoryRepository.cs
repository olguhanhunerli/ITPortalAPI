using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.DTOs.TicketCategoryDTOs;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface ITicketCategoryRepository : IGenericRepository<TicketCategory, ulong>
    {
        Task<bool> ExistsCategoryCodeAsync(string code, ulong? exludeId = null);
        Task<TicketCategory?> GetByIdWithParentTeamAsync(ulong id);
        Task<List<TicketCategoryLookupDTO>> GetCategoryLookUpAsync(string? search, int take, bool activeOnly);
        Task<List<TicketCategory>> GetAllForTreeAsync(bool activeOnly);
    }
}
