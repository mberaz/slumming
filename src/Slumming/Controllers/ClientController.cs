using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Slumming.Models;
using Slumming.Services;
using Microsoft.Extensions.Configuration;

namespace Slumming.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private IClientService clientService;
        private IConfiguration configuration;

        public ClientController(IClientService _clientService, IConfiguration _configuration)
        {
            clientService = _clientService;
            configuration = _configuration;
        }

        //GET api/values
        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            var allclients = await clientService.GetAll();

            var list = allclients.Where(c => c.Apartment.Count == 0)
                .Where(c => !c.IsBusiness)
                .Where(c => c.Name.StartsWith("m"))
                .ToList();

            return list;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Client> Get(int id)
        {
            var connection = configuration["Data:SlummingConnection:ConnectionString"];
            clientService = new ClientService(connection);
            return await clientService.Get(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<int> Post([FromBody]Client value)
        {
            return await clientService.Create(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody]Client value)
        {
            value.Id = id;
            await clientService.Update(value);
            return id;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await clientService.Delete(id);
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Qa { get; set; }
    }
}
