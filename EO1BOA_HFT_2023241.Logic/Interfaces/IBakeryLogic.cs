using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Logic.Interfaces
{
    public interface IBakeryLogic //CRUD + NON-CRUD
    {
        void Create(Bakery bakery);
        Bakery Read(int id);
        IQueryable<Bakery> ReadAll();
        void Update(Bakery bakery);
        void Delete(int id);
        public IQueryable<Oven> OvensByCapacity(int capacity);
        public IQueryable<Bread> AllBreadsFromBakery(string bakery);
        public Oven MostExpensiveOvenInBakery();
        public IQueryable<Bread> AllSweetsFromBakery(string bakery);
        public Bread LightestBread();

    }
}
