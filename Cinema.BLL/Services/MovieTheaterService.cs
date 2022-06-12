using AutoMapper;
using Cinema.BLL.DTO;
using Cinema.DAL.Context;
using Cinema.DAL.Repositories;
using System.Collections.Generic;

namespace Cinema.BLL.Services
{
    public class MovieTheaterService
    {
        private MovieTheaterRepository _repository;
        private IMapper mapper;

        public MovieTheaterService(MovieTheaterRepository repository)
        {
            _repository = repository;

            var configuration = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<MovieTheaterDTO, MovieTheater>()
                    .ForMember(movieTheater => movieTheater.CinemaId, opt => opt.MapFrom(movieTheaterDTO => movieTheaterDTO.MovieTheaterID))
                    .ForMember(movieTheater => movieTheater.CinemaLocation, opt => opt.MapFrom(movieTheaterDTO => movieTheaterDTO.MovieTheaterLocation));
            });

            mapper = new Mapper(configuration);
        }

        public IEnumerable<MovieTheaterDTO> GetAll()
        {
            return mapper.Map<IEnumerable<MovieTheaterDTO>>(_repository.GetAll());
        }

        public MovieTheaterDTO Get(int id)
        {
            return mapper.Map<MovieTheaterDTO>(_repository.Get(id));
        }

        public void Create(MovieTheaterDTO movieTheaterDTO)
        {
            var movieTheater = mapper.Map<MovieTheater>(movieTheaterDTO);
            _repository.AddOrUpdate(movieTheater);
            _repository.Save();
        }

        public void Update(MovieTheaterDTO movieTheaterDTO)
        {
            var movieTheater = mapper.Map<MovieTheater>(movieTheaterDTO);
            _repository.AddOrUpdate(movieTheater);
            _repository.Save();
        }

        public void Delete(MovieTheaterDTO movieTheaterDTO)
        {
            var movieTheater = mapper.Map<MovieTheater>(_repository.Get(movieTheaterDTO.MovieTheaterID));
            _repository.Delete(movieTheater);
            _repository.Save();
        }
    }
}
