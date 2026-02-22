using ITPortal.Entities.Model.Common;

namespace ITPortal.Entities.Model
{
    public class Lookup : AuditableEntity<ulong>
    {
        public ulong LookupTypeId { get; set; }
        public LookupType LookupType { get; set; } = null!;

        public string Code { get; set; } = null!;     
        public string NameTr { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public string? DescriptionTr { get; set; }
        public string? DescriptionEn { get; set; }

        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }
}