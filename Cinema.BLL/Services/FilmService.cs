using AutoMapper;
using Cinema.BLL.DTO;
using Cinema.DAL.Context;
using Cinema.DAL.Repositories;
using System.Collections.Generic;

namespace Cinema.BLL.Services
{
    public class FilmService
    {
        private FilmRepository _repository;
        private IMapper mapper;

        public FilmService(FilmRepository repository)
        {
            _repository = repository;

            var configuration = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<FilmDTO, Film>()
                    .ForMember(film => film.FilmId, opt => opt.MapFrom(filmDTO => filmDTO.FilmId))
                    .ForMember(film => film.FilmTitle, opt => opt.MapFrom(filmDTO => filmDTO.FilmTitle))
                    .ForMember(film => film.FilmDescription, opt => opt.MapFrom(filmDTO => filmDTO.FilmDescription))
                    .ForMember(film => film.FilmReleaseYear, opt => opt.MapFrom(filmDTO => filmDTO.FilmReleaseYear))
                    .ForMember(film => film.FilmDisplayStartDate, opt => opt.MapFrom(filmDTO => filmDTO.FilmDisplayStartDate))
                    .ForMember(film => film.FilmDisplayEndDate, opt => opt.MapFrom(filmDTO => filmDTO.FilmDisplayEndDate))
                    .ForMember(film => film.FilmImage, opt => opt.MapFrom(filmDTO => filmDTO.FilmImage));
            });

            mapper = new Mapper(configuration);
        }

        public IEnumerable<FilmDTO> GetAll()
        {
            return mapper.Map<IEnumerable<FilmDTO>>(_repository.GetAll());
        }

        public FilmDTO Get(int id)
        {
            return mapper.Map<FilmDTO>(_repository.Get(id));
        }

        public void Create(FilmDTO filmDTO)
        {
            var film = mapper.Map<Film>(filmDTO);
            _repository.AddOrUpdate(film);
            _repository.Save();
        }

        public void Update(FilmDTO filmDTO)
        {
            var film = mapper.Map<Film>(filmDTO);
            _repository.AddOrUpdate(film);
            _repository.Save();
        }

        public void Delete(FilmDTO filmDTO)
        {
            var film = mapper.Map<Film>(_repository.Get(filmDTO.FilmId));
            _repository.Delete(film);
            _repository.Save();
        }
    }
}
