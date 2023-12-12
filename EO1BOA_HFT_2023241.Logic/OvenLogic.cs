using EO1BOA_HFT_2023241.Logic.Interfaces;
using EO1BOA_HFT_2023241.Models;
using EO1BOA_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Logic
{
    public class OvenLogic : IOvenLogic
    {
        IRepository<Oven> repo;

        public OvenLogic(IRepository<Oven> repo)
        {
            this.repo = repo;
        }

        public void Create(Oven oven)
        {
            if (oven.Price < 0 || oven.BreadCapacity <= 0 || oven.BakingTime <= 0 )
            {
                throw new ArgumentException();
            }
            this.repo.Create(oven);
        }

        public void Delete(int id)
        {
            if (repo.Read(id) == null)
            {
                throw new Exception();
            }
            this.repo.Delete(id);
        }

        public Oven Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Oven> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Oven oven)
        {
            this.repo.Update(oven);
        }
    }
}
