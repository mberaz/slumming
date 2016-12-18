using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slumming.Models;
using Slumming.DAL.Interfaces;

namespace Slumming.Services
{
    public class DealService : IDealService
    {
        private readonly IDealRepository _dealRepository;

        public DealService(IDealRepository DealRepository)
        {
            _dealRepository = DealRepository;
        }

        public async Task<IEnumerable<Deal>> GetAll()
        {
            return await _dealRepository.GetAllAsync();
        }


        public async Task<Deal> Get(int id)
        {
            return await _dealRepository.GetSingleAsync(id);
        }

        public async Task<int> Create(Deal model)
        {
            _dealRepository.Add(model);
            await _dealRepository.CommitAsync();
            return model.Id;
        }

        public async Task<IEnumerable<int>> Create(List<Deal> model)
        {
            _dealRepository.AddRange(model);
            await _dealRepository.CommitAsync();
            return model.Select(m => m.Id);
        }

        public async Task<int> Update(Deal model)
        {
            var deal = await _dealRepository.GetSingleAsync(model.Id);

            deal.AppartmentId = model.AppartmentId;
            deal.Date = DateTime.Now;
            deal.RegisterId = model.RegisterId;
            deal.SalesmanId = model.SalesmanId;
            deal.Sum = model.Sum;

            await _dealRepository.CommitAsync();
            return model.Id;
        }

        public async Task Delete(int id)
        {
            var Deal = await _dealRepository.GetSingleAsync(id);
            _dealRepository.Delete(Deal);
            await _dealRepository.CommitAsync();
        }
    }
}
