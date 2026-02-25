using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities.Model
{
    public class TicketAttachment
    {
        public ulong Id { get; set; }
        public ulong TicketId { get; set; }
        public Ticket? Ticket { get; set; }
        public ulong UploadedById { get; set; }
        public User? UploadedBy { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public ulong FileSizeBytes { get; set; }
        public ulong StorageProviderId { get; set; }
        public Lookup? StorageProvider { get; set; }
        public string StoragePath{ get; set; }
        public string ChecksumSha256 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
