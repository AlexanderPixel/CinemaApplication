using AutoMapper;
using Cinema.BLL.DTO;
using Cinema.DAL.Context;
using Cinema.DAL.Repositories;
using System.Collections.Generic;

namespace Cinema.BLL.Services
{
    public class UserService
    {
        private UserRepository _repository;
        private IMapper mapper;

        public UserService(UserRepository repository)
        {
            _repository = repository;

            var configuration = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<UserDTO, User>()
                    .ForMember(user => user.UserId, opt => opt.MapFrom(userDTO => userDTO.UserId))
                    .ForMember(user => user.UserLogin, opt => opt.MapFrom(userDTO => userDTO.UserLogin))
                    .ForMember(user => user.UserPassword, opt => opt.MapFrom(userDTO => userDTO.UserPassword))
                    .ForMember(user => user.UserFirstName, opt => opt.MapFrom(userDTO => userDTO.UserFirstName))
                    .ForMember(user => user.UserLastName, opt => opt.MapFrom(userDTO => userDTO.UserLastName))
                    .ForMember(user => user.UserPhone, opt => opt.MapFrom(userDTO => userDTO.UserPhone))
                    .ForMember(user => user.UserEmail, opt => opt.MapFrom(userDTO => userDTO.UserEmail))
                    .ForMember(user => user.IsActive, opt => opt.MapFrom(userDTO => userDTO.IsActive))
                    .ForMember(user => user.UserLastLoginDateTime, opt => opt.MapFrom(userDTO => userDTO.UserLastLoginDateTime))
                    .ReverseMap();
            });

            mapper = new Mapper(configuration);
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return mapper.Map<IEnumerable<UserDTO>>(_repository.GetAll());
        }

        public UserDTO Get(int id)
        {
            return mapper.Map<UserDTO>(_repository.Get(id));
        }

        public void Create(UserDTO userDTO)
        {
            var user = mapper.Map<User>(userDTO);
            _repository.AddOrUpdate(user);
            _repository.Save();
        }

        public void Update(UserDTO userDTO)
        {
            var user = mapper.Map<User>(userDTO);
            _repository.AddOrUpdate(user);
            _repository.Save();
        }

        public void Delete(UserDTO userDTO)
        {
            var user = mapper.Map<User>(_repository.Get(userDTO.UserId));
            user.IsActive = false;
            _repository.AddOrUpdate(user);
            _repository.Save();
        }
    }
}
