using AutoMapper;
using Cinema.BLL.DTO;
using Cinema.DAL.Context;
using Cinema.DAL.Repositories;
using System.Collections.Generic;

namespace Cinema.BLL.Services
{
    public class ActorService
    {
        private ActorRepository _repository;
        private IMapper mapper; 

        public ActorService(ActorRepository repository)
        {
            _repository = repository;

            var configuration = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<ActorDTO, Actor>()
                    .ForMember(actor => actor.ActorId, opt => opt.MapFrom(actorDTO => actorDTO.ActorId))
                    .ForMember(actor => actor.ActorFirstName, opt => opt.MapFrom(actorDTO => actorDTO.ActorFirstName))
                    .ForMember(actor => actor.ActorLastName, opt => opt.MapFrom(actorDTO => actorDTO.ActorLastName))
                    .ForMember(actor => actor.ActorBirthDate, opt => opt.MapFrom(actorDTO => actorDTO.ActorBirthDate));
            });

            mapper = new Mapper(configuration);
        }

        public IEnumerable<ActorDTO> GetAll()
        {
            return mapper.Map<IEnumerable<ActorDTO>>(_repository.GetAll());
        }

        public ActorDTO Get(int id)
        {
            return mapper.Map<ActorDTO>(_repository.Get(id));
        }

        public void Create(ActorDTO actorDTO)
        {
            var actor = mapper.Map<Actor>(actorDTO);
            _repository.AddOrUpdate(actor);
            _repository.Save();
        }

        public void Update(ActorDTO actorDTO)
        {
            var actor = mapper.Map<Actor>(actorDTO);
            _repository.AddOrUpdate(actor);
            _repository.Save();
        }

        public void Delete(ActorDTO actorDTO)
        {
            var actor = mapper.Map<Actor>(_repository.Get(actorDTO.ActorId));
            _repository.Delete(actor);
            _repository.Save();
        }
    }
}
