using Cinema.DAL.Context;
using System.Data.Entity;

namespace Cinema.DAL.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>
    {
        public TicketRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
