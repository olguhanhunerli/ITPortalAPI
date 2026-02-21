using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.Interfaces
{
    public interface ILookupRepository : IGenericRepository<Lookup,ulong>
    {
        Task<List<LookupType>> GetLookupTypesAsync(string? search, int take);
        Task<List<Lookup>> GetLookupsByTypeCodeAsync(string typeCode, string? search, int take);
        Task<bool> ExistsAsync(string typeCode, string code, ulong? excludeId = null);
    }
}
