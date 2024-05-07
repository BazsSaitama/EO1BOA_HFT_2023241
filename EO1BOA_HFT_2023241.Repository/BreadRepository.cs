using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Repository
{
    public class BreadRepository : Repository<Bread>, IRepository<Bread>
    {
        public BreadRepository(BakeryDbContext ctor) : base(ctor)
        {
        }
        public override Bread Read(int id)
        {
            return dbcontext.Bread.FirstOrDefault(x => x.BreadId == id);
        }

        public override void Update(Bread obj)
        {
            var old = Read(obj.BreadId);
            foreach (var item in obj.GetType().GetProperties())
            {
                item.SetValue(old, item.GetValue(obj));
            }
            dbcontext.SaveChanges();
        }
    }
}
