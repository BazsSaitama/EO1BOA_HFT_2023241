using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Repository
{
    public class BakeryRepository : Repository<Bakery>
    {
        public BakeryRepository(BakeryDbContext ctor) : base(ctor)
        {
        }
        public override Bakery Read(int id)
        {
            return dbcontext.Bakeries.FirstOrDefault(x=>x.BakeryId == id);
        }

        public override void Update(Bakery obj)
        {
            var old = Read(obj.BakeryId);
            foreach (var item in obj.GetType().GetProperties())
            {
                item.SetValue(old, item.GetValue(obj));
            }
            dbcontext.SaveChanges();
        }
    }
}