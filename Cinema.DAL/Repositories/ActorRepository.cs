using Cinema.DAL.Context;
using System.Data.Entity;

namespace Cinema.DAL.Repositories
{
    public class ActorRepository : GenericRepository<Actor>
    {
        public ActorRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
