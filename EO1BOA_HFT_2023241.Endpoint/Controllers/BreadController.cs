using EO1BOA_HFT_2023241.Logic.Interfaces;
using EO1BOA_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BreadController : ControllerBase
    {
        
        IBreadLogic logic;
        public BreadController(IBreadLogic logic)
        {
            this.logic = logic;
        }
        [HttpPost]
        public void Create([FromBody] Bread value)
        {
            this.logic.Create(value);
        }
        
        
        [HttpGet]
        public IEnumerable<Bread> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Bread Read(int id)
        {
            return this.logic.Read(id);
        }
        [HttpPut]
        public void Put([FromBody] Bread value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
