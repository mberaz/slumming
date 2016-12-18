

using Slumming.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slumming.Services
{
    public interface ISalesmanService
    {
        Task<IEnumerable<Salesman>> GetAll ();

        Task<Salesman> Get (int id);

        Task<int> Create (Salesman model);

        Task<IEnumerable<int>> Create (List<Salesman> model);
        Task<int> Update(Salesman model);

        Task Delete(int id);
    }
}
