

using Slumming.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slumming.Services
{
    public interface IApartmentService
    {
        Task<IEnumerable<Apartment>> GetAll ();

        Task<Apartment> Get (int id);

        Task<int> Create (Apartment model);

        Task<IEnumerable<int>> Create (List<Apartment> model);

        Task<int> Update (Apartment model);
    }
}
