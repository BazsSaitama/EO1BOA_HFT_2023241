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
    public class BreadLogic : IBreadLogic
    {
        IRepository<Bread> repo;

        public BreadLogic(IRepository<Bread> repo)
        {
            this.repo = repo;
        }

        public void Create(Bread bread)
        {
            if (bread.Weight <= 0)
            {
                throw new ArgumentException();
            }
            this.repo.Create(bread);
        }

        public void Delete(int id)
        {
            if (repo.Read(id) == null)
            {
                throw new Exception();
            }
            this.repo.Delete(id);
        }

        public Bread Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Bread> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Bread bread)
        {
            this.repo.Update(bread);
        }
    }
}
