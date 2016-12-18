using Microsoft.AspNetCore.Mvc;
using Slumming.Models;
using Slumming.Providers;
using Slumming.Services;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Slumming.Controllers
{
    [Route("api/[controller]")]
    public class DealController : Controller
    {
        private IClientService clientService;
        private ISalesmanService salesmanService;
        private IDealService dealService;
        private IApartmentService apartmentService;


        public DealController(IClientService _clientService, ISalesmanService _salesmanService, IDealService _dealService, IApartmentService _apartmentService)
        {
            clientService = _clientService;
            salesmanService = _salesmanService;
            dealService = _dealService;
            apartmentService = _apartmentService;
        }

        // POST api/values
        [HttpPost]
        public async Task<FullDealResult> Post([FromBody]FullDeal value)
        {
            var provider = new FullDealProvider(salesmanService, clientService, dealService, apartmentService);
            var result = await provider.CreateDeal(value);
            return new FullDealResult { DealId = result.Deal.Id, isValid = result.IsValid, Reason = result.Reason };
        }
    }
}
