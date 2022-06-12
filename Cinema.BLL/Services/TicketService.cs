using AutoMapper;
using Cinema.BLL.DTO;
using Cinema.DAL.Context;
using Cinema.DAL.Repositories;
using System.Collections.Generic;
namespace Cinema.BLL.Services
{
    public class TicketService
    {
        private TicketRepository _repository;
        private IMapper mapper;

        public TicketService(TicketRepository repository)
        {
            _repository = repository;

            var configuration = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<TicketDTO, Ticket>()
                    .ForMember(ticket => ticket.TicketId, opt => opt.MapFrom(ticketDTO => ticketDTO.TicketId))
                    .ForMember(ticket => ticket.UserId, opt => opt.MapFrom(ticketDTO => ticketDTO.UserId))
                    .ForMember(ticket => ticket.FilmId, opt => opt.MapFrom(ticketDTO => ticketDTO.FilmId))
                    .ForMember(ticket => ticket.CinemaHallId, opt => opt.MapFrom(ticketDTO => ticketDTO.CinemaHallId))
                    .ForMember(ticket => ticket.MovieTheaterId, opt => opt.MapFrom(ticketDTO => ticketDTO.MovieTheaterId))
                    .ForMember(ticket => ticket.TicketDate, opt => opt.MapFrom(ticketDTO => ticketDTO.TicketDate))
                    .ReverseMap();
            });

            mapper = new Mapper(configuration);
        }

        public IEnumerable<TicketDTO> GetAll()
        {
            return mapper.Map<IEnumerable<TicketDTO>>(_repository.GetAll());
        }

        public TicketDTO Get(int id)
        {
            return mapper.Map<TicketDTO>(_repository.Get(id));
        }

        public void Create(TicketDTO ticketDTO)
        {
            var ticket = mapper.Map<Ticket>(ticketDTO);
            _repository.AddOrUpdate(ticket);
            _repository.Save();
        }

        public void Update(TicketDTO ticketDTO)
        {
            var ticket = mapper.Map<Ticket>(ticketDTO);
            _repository.AddOrUpdate(ticket);
            _repository.Save();
        }

        public void Delete(TicketDTO ticketDTO)
        {
            var ticket = mapper.Map<Ticket>(_repository.Get(ticketDTO.TicketId));
            _repository.AddOrUpdate(ticket);
            _repository.Save();
        }
    }
}
