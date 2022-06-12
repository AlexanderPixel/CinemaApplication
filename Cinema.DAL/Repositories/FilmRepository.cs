using Cinema.DAL.Context;
using System.Data.Entity;

namespace Cinema.DAL.Repositories
{
    public class FilmRepository : GenericRepository<Film>
    {
        public FilmRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
