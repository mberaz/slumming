

using Slumming.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slumming.Services
{
    public interface IDealService
    {
        Task<IEnumerable<Deal>> GetAll ();

        Task<Deal> Get (int id);

        Task<int> Create (Deal model);

        Task<IEnumerable<int>> Create (List<Deal> model);
        Task<int> Update(Deal model);

        Task Delete(int id);
    }
}
