using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Cinema.DAL.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private DbContext dbContext;
        private DbSet<T> dbSet;

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll() => dbSet;

        public T Get(int id) => dbSet.Find(id);

        public void AddOrUpdate(T item) => dbSet.AddOrUpdate(item);

        public void Delete(T item) => dbSet.Remove(item);

        public void Save() => dbContext.SaveChanges();
    }
}
