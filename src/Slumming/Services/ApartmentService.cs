using Slumming.DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slumming.Models;

namespace Slumming.Services
{
    public class ApartmentService :IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentService (IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<IEnumerable<Apartment>> GetAll ()
        {
            return await _apartmentRepository.GetAllAsync();
        }


        public async Task<Apartment> Get (int id)
        {
            return await _apartmentRepository.GetSingleAsync(id);
        }

        public async Task<int> Create (Apartment model)
        {
            _apartmentRepository.Add(model);

            await _apartmentRepository.CommitAsync();

            return model.Id;
        }

        public async Task<IEnumerable<int>> Create (List<Apartment> model)
        {
            _apartmentRepository.AddRange(model);

            await _apartmentRepository.CommitAsync();

            return model.Select(m => m.Id);
        }

        public async Task<int> Update (Apartment model)
        {
            var apartment = await _apartmentRepository.GetSingleAsync(model.Id);

            apartment.Name = model.Name;
            apartment.Appartment = model.Appartment;
            apartment.HouseNumber = model.HouseNumber;
            apartment.StateId = model.StateId;
            apartment.CityId = model.CityId;
            apartment.Street = model.Street;
            apartment.ClientId = model.ClientId;
            apartment.IsInsured = model.IsInsured;
            apartment.OwnerName = model.OwnerName;

            apartment.Price = model.Price;

            await _apartmentRepository.CommitAsync();
            return model.Id;
        }

    }
}
