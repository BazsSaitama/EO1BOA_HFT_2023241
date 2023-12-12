using ConsoleTools;
using EO1BOA_HFT_2023241.Models;
using System;

namespace EO1BOA_HFT_2023241.Client
{
    class Program
    {

        static RestService rest;
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:39340/");
            CrudService CRUD = new CrudService(rest);
            NonCrudService NonCRUD = new NonCrudService(rest);

            var BakeryMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => CRUD.List<Bakery>())
                .Add("Create", () => CRUD.Create<Bakery>())
                .Add("Delete", () => CRUD.Delete<Bakery>())
                .Add("Update", () => CRUD.Update<Bakery>())
                .Add("Exit", ConsoleMenu.Close);
            var BreadMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => CRUD.List<Bread>())
                .Add("Create", () => CRUD.Create<Bread>())
                .Add("Delete", () => CRUD.Delete<Bread>())
                .Add("Update", () => CRUD.Update<Bread>())
                .Add("Exit", ConsoleMenu.Close);
            var OvenMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => CRUD.List<Oven>())
                .Add("Create", () => CRUD.Create<Oven>())
                .Add("Delete", () => CRUD.Delete<Oven>())
                .Add("Update", () => CRUD.Update<Oven>())
                .Add("Exit", ConsoleMenu.Close);
            var NonCrudMenu = new ConsoleMenu(args, level: 1)
                //public IQueryable<Oven> OvensByCapacity(int capacity);
                //public IQueryable<Bread> AllBreadsFromBakery(string bakery);
                //public Oven MostExpensiveOvenInBakery();
                //public IQueryable<Bread> AllSweetsFromBakery(string bakery);
                //public Bread LightestBread();
                .Add("OvensByCapatity", () => NonCRUD.OvensByCapacity())
                .Add("AllBreadsFromBakery", () => NonCRUD.AllBreadsFromBakery())
                .Add("MostExpensiveOvenInBakery", () => NonCRUD.MostExpensiveOvenInBakery())
                .Add("AllBreadsFromBakery", () => NonCRUD.AllBreadsFromBakery())
                .Add("LightestBread", () => NonCRUD.LightestBread())
                .Add("Exit", ConsoleMenu.Close);
            var menu = new ConsoleMenu(args, level: 0)
                .Add("Bakeries", () => BakeryMenu.Show())
                .Add("Breads", () => BreadMenu.Show())
                .Add("Ovens", () => OvenMenu.Show())
                .Add("NonCrud", () => NonCrudMenu.Show());
            menu.Show();
        }
    }
}
