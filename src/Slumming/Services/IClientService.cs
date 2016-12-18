

using Slumming.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slumming.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAll ();

        Task<Client> Get (int id);

        Task<int> Create (Client model);

        Task<IEnumerable<int>> Create (List<Client> model);
        Task<int> Update(Client model);

        Task Delete(int id);
    }
}
