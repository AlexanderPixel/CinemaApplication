using AutoMapper;
using Cinema.BLL.DTO;
using Cinema.DAL.Context;
using Cinema.DAL.Repositories;
using System.Collections.Generic;

namespace Cinema.BLL.Services
{
    public class AdminService
    {
        private AdminRepository _repository;
        private IMapper mapper;

        public AdminService(AdminRepository repository)
        {
            _repository = repository;

            var configuration = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<AdminDTO, Admin>()
                    .ForMember(admin => admin.AdminId, opt => opt.MapFrom(adminDTO => adminDTO.AdminId))
                    .ForMember(admin => admin.UserId, opt => opt.MapFrom(adminDTO => adminDTO.UserId));
            });

            mapper = new Mapper(configuration);
        }

        public IEnumerable<AdminDTO> GetAll()
        {
            return mapper.Map<IEnumerable<AdminDTO>>(_repository.GetAll());
        }

        public AdminDTO Get(int id)
        {
            return mapper.Map<AdminDTO>(_repository.Get(id));
        }

        public void Create(AdminDTO adminDTO)
        {
            var admin = mapper.Map<Admin>(adminDTO);
            _repository.AddOrUpdate(admin);
            _repository.Save();
        }

        public void Update(AdminDTO adminDTO)
        {
            var admin = mapper.Map<Admin>(adminDTO);
            _repository.AddOrUpdate(admin);
            _repository.Save();
        }

        public void Delete(AdminDTO adminDTO)
        {
            var admin = mapper.Map<Admin>(_repository.Get(adminDTO.AdminId));
            _repository.Delete(admin);
            _repository.Save();
        }
    }
}
