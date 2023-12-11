using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Repository
{
    public interface IRepository<T> where T:class
    {
        void Create(T obj);
        T Read(int id);
        void Update(T obj);
        void Delete(int id);

        IQueryable<T> ReadAll();
    }
}
