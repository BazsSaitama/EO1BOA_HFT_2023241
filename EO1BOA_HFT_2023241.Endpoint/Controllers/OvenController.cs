using EO1BOA_HFT_2023241.Endpoint.Services;
using EO1BOA_HFT_2023241.Logic.Interfaces;
using EO1BOA_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OvenController : ControllerBase
    {
        IHubContext<SignalRHub> hub;
        IOvenLogic logic;

        public OvenController(IOvenLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpPost]
        public void Create([FromBody] Oven value)
        {
            this.logic.Create(value);
            hub.Clients.All.SendAsync("OvenCreated", value);
        }
        [HttpGet]
        public IEnumerable<Oven> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Oven Read(int id)
        {
            return this.logic.Read(id);
        }

        

        [HttpPut]
        public void Update([FromBody] Oven value)
        {
            this.logic.Update(value);
            hub.Clients.All.SendAsync("OvenUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var value = logic.Read(id);
            this.logic.Delete(id);
            hub.Clients.All.SendAsync("OvenDeleted", value);
        }
    }
}
