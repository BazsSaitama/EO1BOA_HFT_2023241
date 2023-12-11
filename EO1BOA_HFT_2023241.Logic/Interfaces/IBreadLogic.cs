using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Logic.Interfaces
{
    public interface IBreadLogic//CRUD
    {
        void Create(Bread bread);
        Bread Read(int id);
        IQueryable<Bread> ReadAll();
        void Update(Bread bread);
        void Delete(int id);

    }
}
