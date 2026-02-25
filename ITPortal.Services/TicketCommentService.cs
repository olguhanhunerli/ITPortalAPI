using AutoMapper;
using ITPortal.Business.Repository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.TicketCommentDTOs;
using ITPortal.Entities.Model;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class TicketCommentService : ITicketCommentService
    {
        private readonly ITicketCommentRepository _ticketCommentRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITicketEventRepository _ticketEventRepository;
        public TicketCommentService(ITicketCommentRepository ticketCommentRepository, ITicketRepository ticketRepository, IUserRepository userRepository, IMapper mapper, ITicketEventRepository ticketEventRepository)
        {
            _ticketCommentRepository = ticketCommentRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _ticketEventRepository = ticketEventRepository;
        }

        public async Task<TicketCommentDTO> CreateTicketCommentAsync(ulong ticketId, CreateCommentDTO createTicketCommentDTO, ulong userId)
        {
            var ticket = await _ticketRepository.GetByTicketIdAsync(ticketId);
            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if(ticket.RequesterId != user.Id)
            {
                throw new Exception("Ticket size ait değildir.");
            }
            var ticketComment = new TicketComment
            {
                TicketId = ticketId,
                AuthorId = userId,
                Body = createTicketCommentDTO.Body,
                CreatedAt = DateTime.UtcNow,
                VisibilityId = createTicketCommentDTO.VisibilityId
            };
            await _ticketCommentRepository.AddAsync(ticketComment);
            await _ticketCommentRepository.SaveChangesAsync();
            await _ticketEventRepository.AddAsync(new TicketEvent
            {
                TicketId = ticketId,
                EventType = "Yorum Eklendi",
                ActorId = userId,
                PayloadJson = JsonSerializer.Serialize(new
                {
                    message = "CommentAdded",

                }),
                CreatedAt = DateTime.UtcNow
            });
            await _ticketEventRepository.SaveChangesAsync();
            var createdComment = await _ticketCommentRepository.GetDetailByIdAsync(ticketComment.Id);
            return _mapper.Map<TicketCommentDTO>(createdComment);
        }
    }
}
