using System.Data.Entity;
using Cinema.DAL.Context;

namespace Cinema.DAL.Repositories
{
    public class MovieTheaterRepository : GenericRepository<MovieTheater>
    {
        public MovieTheaterRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
