using Cinema.DAL.Context;
using System.Data.Entity;

namespace Cinema.DAL.Repositories
{
    public class CinemaHallRepository : GenericRepository<CinemaHall>
    {
        public CinemaHallRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
