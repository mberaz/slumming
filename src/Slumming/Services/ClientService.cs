using Slumming.DAL.Interfaces;
using Slumming.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Slumming.DAL.Repositories;

namespace Slumming.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public ClientService(string connection)
        {
            _clientRepository = new ClientRepository(new SlummingContext(connection));
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _clientRepository.GetAllAsync();
        }


        public async Task<Client> Get(int id)
        {
            return await _clientRepository.GetSingleAsync(id);
        }

        public async Task<int> Create(Client model)
        {
            _clientRepository.Add(model);
            await _clientRepository.CommitAsync();
            return model.Id;
        }

        public async Task<IEnumerable<int>> Create(List<Client> model)
        {
            _clientRepository.AddRange(model);
            await _clientRepository.CommitAsync();
            return model.Select(m => m.Id);
        }

        public async Task<int> Update(Client model)
        {
            var client = await _clientRepository.GetSingleAsync(model.Id);

            client.Address = model.Address;
            client.IsBusiness = model.IsBusiness;
            client.Name = model.Name;
            client.BillingAdsress = model.BillingAdsress;

            await _clientRepository.CommitAsync();
            return model.Id;
        }

        public async Task Delete(int id)
        {
            var client = await _clientRepository.GetSingleAsync(id);
            _clientRepository.Delete(client);
            await _clientRepository.CommitAsync();
        }
    }
}
