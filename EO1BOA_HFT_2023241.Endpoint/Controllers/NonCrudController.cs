using EO1BOA_HFT_2023241.Logic.Interfaces;
using EO1BOA_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    public class NonCrudController : ControllerBase
    {
        IBakeryLogic logic;


        public NonCrudController(IBakeryLogic logic)
        {
            this.logic = logic;
        }
        //public IQueryable<Oven> OvensByCapacity(int capacity);
        //public IQueryable<Bread> AllBreadsFromBakery(string bakery);
        //public Oven MostExpensiveOvenInBakery();
        //public IQueryable<Bread> AllSweetsFromBakery(string bakery);
        //public Bread LightestBread();

        [HttpGet("{capacity}")]
        public IQueryable<Oven> OvensByCapacity(int capacity)
        {
            return logic.OvensByCapacity(capacity);
        }

        [HttpGet("{bakery}")]
        public IQueryable<Bread> AllBreadsFromBakery(string bakery)
        {
            return logic.AllBreadsFromBakery(bakery);
        }

        [HttpGet]
        public Oven MostExpensiveOvenInBakery()
        {
            return logic.MostExpensiveOvenInBakery();
        }

        [HttpGet("{bakery}")]
        public IQueryable<Bread> AllSweetsFromBakery([FromQuery] string bakery)
        {
            return logic.AllSweetsFromBakery(bakery);
        }

        [HttpGet]
        public Bread LightestBread()
        {
            return logic.LightestBread();
        }
    }
}
