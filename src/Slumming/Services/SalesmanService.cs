using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slumming.Models;
using Slumming.DAL.Interfaces;

namespace Slumming.Services
{
    public class SalesmanService : ISalesmanService
    {
        private readonly ISalesmanRepository _salesmanRepository;

        public SalesmanService(ISalesmanRepository SalesmanRepository)
        {
            _salesmanRepository = SalesmanRepository;
        }

        public async Task<IEnumerable<Salesman>> GetAll()
        {
            return await _salesmanRepository.GetAllAsync();
        }


        public async Task<Salesman> Get(int id)
        {
            return await _salesmanRepository.GetSingleAsync(id);
        }

        public async Task<int> Create(Salesman model)
        {
            _salesmanRepository.Add(model);
            await _salesmanRepository.CommitAsync();
            return model.Id;
        }

        public async Task<IEnumerable<int>> Create(List<Salesman> model)
        {
            _salesmanRepository.AddRange(model);
            await _salesmanRepository.CommitAsync();
            return model.Select(m => m.Id);
        }

        public async Task<int> Update(Salesman model)
        {
            var Salesman = await _salesmanRepository.GetSingleAsync(model.Id);

            Salesman.Name = model.Name;
            Salesman.Commision = model.Commision;

            await _salesmanRepository.CommitAsync();
            return model.Id;
        }

        public async Task Delete(int id)
        {
            var Salesman = await _salesmanRepository.GetSingleAsync(id);
            _salesmanRepository.Delete(Salesman);
            await _salesmanRepository.CommitAsync();
        }
    }
}
