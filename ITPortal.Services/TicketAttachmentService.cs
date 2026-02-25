using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.TicketAttachmentDTOs;
using ITPortal.Entities.Model;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class TicketAttachmentService : ITicketAttachmentService
    {
        private readonly ITicketAttachmentRepository _ticketAttachmentRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public TicketAttachmentService(ITicketRepository ticketRepository, ITicketAttachmentRepository ticketAttachmentRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketAttachmentRepository = ticketAttachmentRepository;
            _userRepository = userRepository;
        }

        public async Task<TicketAttachmentMiniDTO> CreateTicketAttachmentAsyn(ulong ticketId, CreateTicketAttachmentDTO dto, ulong uploadedBy, CancellationToken ct = default)
        {
            var ticket = await _ticketRepository.GetByTicketIdAsync(ticketId);
            if (ticket == null)
                throw new Exception($"Ticket with ID {ticketId} not found.");

            if (dto?.File == null || dto.File.Length == 0)
                throw new Exception("File is required.");

            const long maxBytes = 25 * 1024 * 1024;
            if (dto.File.Length > maxBytes)
                throw new Exception("File size exceeds limit.");

            var originalFileName = Path.GetFileName(dto.File.FileName);
            var safeFileName = SanitizeFileName(originalFileName);
            var storedFileName = $"{Guid.NewGuid():N}";

            var relativeFolder = Path.Combine("uploads", "tickets", ticket.TicketNumber);
            var absoluteFolder = Path.Combine(Directory.GetCurrentDirectory(), relativeFolder);

            if (!Directory.Exists(absoluteFolder))
                Directory.CreateDirectory(absoluteFolder);

            var absolutePath = Path.Combine(absoluteFolder, storedFileName);
            var relativePath = Path.Combine(relativeFolder, storedFileName).Replace("\\", "/");

            await using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream, ct);
            }

            var entity = new TicketAttachment
            {
                TicketId = ticket.Id,
                UploadedById = uploadedBy,
                ContentType = dto.File.ContentType ?? "application/octet-stream",
                FileSizeBytes = (ulong)dto.File.Length,
                StorageProviderId = 1,                
                StoragePath = relativePath,
                ChecksumSha256 = null,                
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                FileName = ticket.TicketNumber
            };

            await _ticketAttachmentRepository.AddAsync(entity, ct);
            await _ticketAttachmentRepository.SaveChangesAsync(ct);

            return new TicketAttachmentMiniDTO
            {
                Id = entity.Id,
                FileName = entity.FileName,
                ContentType = entity.ContentType,
                FileSizeBytes = (long)entity.FileSizeBytes,
                CreatedAt = entity.CreatedAt
            };

        }
        private static string SanitizeFileName(string fileName)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                fileName = fileName.Replace(c, '_');

            if (fileName.Length > 180)
                fileName = fileName[..180];

            return fileName;
        }
        public Task<TicketAttachmentMiniDTO> GetTicketAttachmentMini(ulong ticketId)
        {
            throw new NotImplementedException();
        }
    }
}
