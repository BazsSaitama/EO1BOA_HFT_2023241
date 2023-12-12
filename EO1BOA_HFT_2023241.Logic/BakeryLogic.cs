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
    public class BakeryLogic : IBakeryLogic //CRUD + NON-CRUD
    {
        IRepository<Bakery> repo;

        public BakeryLogic(IRepository<Bakery> repo)
        {
            this.repo = repo;
        }

        public void Create(Bakery bakery)
        {
            if (bakery.Name.Length > 99 || bakery.Name.Length < 0)
            {
                throw new ArgumentException();
            }
            else if (bakery.Location == null || bakery.Location == "")
            {
                throw new Exception();
            }
            this.repo.Create(bakery);
        }

        public void Delete(int id)
        {
            if (repo.Read(id) == null)
            {
                throw new Exception();
            }
            this.repo.Delete(id);
        }

        public Bakery Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Bakery> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Bakery bakery)
        {
            this.repo.Update(bakery);
        }

        public IQueryable<Oven> OvensByCapacity(int capacity)
        {
            var ovenlist = from b in repo.ReadAll()
                           from bread in b.Breads
                           from d in bread.Ovens
                           where d.BreadCapacity == capacity
                           select d;
            return ovenlist;
        }
        public IQueryable<Bread> AllBreadsFromBakery(string bakery)
        {
            var breads = from b in repo.ReadAll()
                         where b.Name == bakery
                         from bread in b.Breads
                         select bread;
            return breads;
        }
        public Oven MostExpensiveOvenInBakery(string bakery) 
        {
            var money = from b in repo.ReadAll()
                        from breads in b.Breads
                        from oven in breads.Ovens
                        orderby oven.Price descending
                        select oven; 
            return money.First();
        }
        public IQueryable<Bread> AllSweetsFromBakery(string bakery)
        {
            var sweets = from b in repo.ReadAll()
                         from breads in b.Breads
                         where breads.IsDessert == true
                         select breads;
            return sweets;
        }
        public Bread LightestBread(string bakery) 
        {
            var bread = from b in repo.ReadAll()
                        from d in b.Breads
                        where b.Name == bakery
                        orderby d.Weight ascending
                        select d;
            return bread.First();
        }


    }
}
