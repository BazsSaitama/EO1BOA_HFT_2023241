﻿using EO1BOA_HFT_2023241.Endpoint.Services;
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
    public class BakeryController : ControllerBase
    {
        IBakeryLogic logic;
        IHubContext<SignalRHub> hub;

        public BakeryController(IBakeryLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }
        [HttpPost]
        public void Create([FromBody] Bakery value)
        {
            this.logic.Create(value);
            hub.Clients.All.SendAsync("BakeryCreated", value);
        }

        [HttpGet]
        public IEnumerable<Bakery> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Bakery Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPut]
        public void Update([FromBody] Bakery value)
        {
            this.logic.Update(value);
            hub.Clients.All.SendAsync("BakeryUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var value = logic.Read(id);
            this.logic.Delete(id);
            hub.Clients.All.SendAsync("BakeryDeleted", value);
        }
    }
}
