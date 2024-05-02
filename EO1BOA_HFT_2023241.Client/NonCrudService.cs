using EO1BOA_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EO1BOA_HFT_2023241.Client
{
    class NonCrudService
    {
        private RestService restService;

        public NonCrudService(RestService restService)
        {
            this.restService = restService;
        }
        //public IQueryable<Oven> OvensByCapacity(int capacity);
        //public IQueryable<Bread> AllBreadsFromBakery(string bakery);
        //public Oven MostExpensiveOvenInBakery();
        //public IQueryable<Bread> AllSweetsFromBakery(string bakery);
        //public Bread LightestBread();

        public void OvensByCapacity()
        {
            try
            {
                Console.WriteLine("Please give a number to List the according ovens! [2,14,21]");
                string ovens = Console.ReadLine();
                var items = restService.Get<Oven>($"/NonCrud/OvensByCapacity/{ovens}");
                foreach (var item in items)
                {
                    Console.WriteLine(item.OvenId + "\t" + item.BreadCapacity);
                }
                
            }
            catch (Exception)
            {

                Console.WriteLine("Error! Oven not found!");
            }
            Console.WriteLine("");
            Console.WriteLine("Press any button to return");
            Console.ReadKey();
            
        }
        public void AllBreadsFromBakery()
        {
            try
            {
                Console.WriteLine("To List all Breads, please enter a bakery[Szaszkó Pékség,Müller Pékség,Niki Pékség, Champion Pékség]:");
                string breads = Console.ReadLine();
                var items = restService.Get<Bread>($"/NonCrud/AllBreadsFromBakery/{breads}");
                foreach (var item in items)
                {
                    Console.WriteLine(item.Name + "\t" + item.Weight);
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error! Bakery not Found!");
            }
            Console.WriteLine("");
            Console.WriteLine("Press any button to return");
            Console.ReadKey();
        }
        public void MostExpensiveOvenInBakery()
        {
            Console.WriteLine("Most Expensive Oven in the Bakery:");
            var item = restService.GetSingle<Oven>($"/NonCrud/MostExpensiveOvenInBakery/");
            Console.WriteLine("OvenID: " + item.OvenId + "\t" + item.Price);
            Console.WriteLine("");
            Console.WriteLine("Press any button to return");
            Console.ReadKey();
        }
        public void AllSweetsFromBakery()
        {
            

            try
            {
                Console.WriteLine("All the sweet from bakery: [Szaszkó Pékség,Müller Pékség,Niki Pékség, Champion Pékség]");
                string bakery = Console.ReadLine();
                var bakeries = restService.Get<Bread>($"/NonCrud/AllSweetsFromBakery/{bakery}");
                foreach (var item in bakeries)
                {
                    Console.WriteLine(item.Name + "\t" + item.Weight);
                }
                
            }
            catch (Exception)
            {

                Console.WriteLine("Error! Bakery not found!");
            }
            Console.WriteLine("");
            Console.WriteLine("Press any button to return");
            Console.ReadKey();
        }
        public void LightestBread()
        {
            Console.WriteLine("The lightest bread:");
            var item = restService.GetSingle<Bread>($"/NonCrud/LightestBread/");
            Console.WriteLine(item.Name + "\t" + item.Weight);
            Console.WriteLine("");
            Console.WriteLine("Press any button to return");
            Console.ReadKey();
        }
    }
}
