namespace ITPortal.Entities.DTOs.TicketAttachmentDTOs
{
    public class TicketAttachmentMiniDTO
    {
        public ulong Id { get; set; }
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSizeBytes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
