using Cinema.DAL.Context;
using System.Data.Entity;

namespace Cinema.DAL.Repositories
{
    public class AdminRepository : GenericRepository<Admin>
    {
        public AdminRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
