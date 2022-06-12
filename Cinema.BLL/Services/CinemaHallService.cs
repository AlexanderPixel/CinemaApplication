using AutoMapper;
using Cinema.BLL.DTO;
using Cinema.DAL.Context;
using Cinema.DAL.Repositories;
using System.Collections.Generic;

namespace Cinema.BLL.Services
{
    public class CinemaHallService
    {
        private CinemaHallRepository _repository;
        private IMapper mapper;

        public CinemaHallService(CinemaHallRepository repository)
        {
            _repository = repository;

            var configuration = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<CinemaHallDTO, CinemaHall>()
                    .ForMember(cinemaHall => cinemaHall.CinemaHallId, opt => opt.MapFrom(cinemaHallDTO => cinemaHallDTO.CinemaHallId))
                    .ForMember(cinemaHall => cinemaHall.TotalSeatsNumber, opt => opt.MapFrom(cinemaHallDTO => cinemaHallDTO.TotalSeatsNumber))
                    .ForMember(cinemaHall => cinemaHall.AvailableSeatsNumber, opt => opt.MapFrom(cinemaHallDTO => cinemaHallDTO.AvailableSeatsNumber))
                    .ForMember(cinemaHall => cinemaHall.BookedSeatsNumber, opt => opt.MapFrom(cinemaHallDTO => cinemaHallDTO.BookedSeatsNumber))
                    .ForMember(cinemaHall => cinemaHall.CinemaId, opt => opt.MapFrom(cinemaHallDTO => cinemaHallDTO.CinemaId));
            });

            mapper = new Mapper(configuration);
        }

        public IEnumerable<CinemaHallDTO> GetAll()
        {
            return mapper.Map<IEnumerable<CinemaHallDTO>>(_repository.GetAll());
        }

        public CinemaHallDTO Get(int id)
        {
            return mapper.Map<CinemaHallDTO>(_repository.Get(id));
        }

        public void Create(CinemaHallDTO cinemaHallDTO)
        {
            var cinemaHall = mapper.Map<CinemaHall>(cinemaHallDTO);
            _repository.AddOrUpdate(cinemaHall);
            _repository.Save();
        }

        public void Update(CinemaHallDTO cinemaHallDTO)
        {
            var cinemaHall = mapper.Map<CinemaHall>(cinemaHallDTO);
            _repository.AddOrUpdate(cinemaHall);
            _repository.Save();
        }

        public void Delete(CinemaHallDTO cinemaHallDTO)
        {
            var cinemaHall = mapper.Map<CinemaHall>(_repository.Get(cinemaHallDTO.CinemaHallId));
            _repository.Delete(cinemaHall);
            _repository.Save();
        }
    }
}
