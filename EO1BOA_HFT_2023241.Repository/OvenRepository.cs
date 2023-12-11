using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Repository
{
    public class OvenRepository : Repository<Oven>
    {
        public OvenRepository(BakeryDbContext ctor) : base(ctor)
        {
        }

        public override Oven Read(int id)
        {
            return dbcontext.Ovens.FirstOrDefault(x => x.OvenId == id);
        }

        public override void Update(Oven obj)
        {
            var old = Read(obj.OvenId);
            foreach (var item in old.GetType().GetProperties())
            {
                item.SetValue(old, item.GetValue(obj));
            }
        }
    }
}
