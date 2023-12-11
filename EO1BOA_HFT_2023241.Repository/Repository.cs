using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected BakeryDbContext dbcontext;
        public Repository(BakeryDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public void Create(T obj)
        {
            dbcontext.Set<T>().Add(obj);
            dbcontext.SaveChanges();
        }

        public void Delete(int id)
        {
            dbcontext.Set<T>().Remove(Read(id));
        }

        public abstract T Read(int id);

        public IQueryable<T> ReadAll()
        {
            return dbcontext.Set<T>();
        }

        public abstract void Update(T obj);
    }
}
