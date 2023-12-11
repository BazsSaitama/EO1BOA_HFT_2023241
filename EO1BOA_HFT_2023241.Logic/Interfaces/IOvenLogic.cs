using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Logic.Interfaces
{
    public interface IOvenLogic//CRUD
    {
        void Create(Oven oven);
        Oven Read(int id);
        IQueryable<Oven> ReadAll();
        void Update(Oven oven);
        void Delete(int id);
    }
}
