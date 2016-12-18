using Microsoft.EntityFrameworkCore;
using Slumming.DAL.Interfaces;
using Slumming.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Slumming.DAL.Repositories
{

    public class StateRepository :EntityBaseRepository<State>, IStateRepository
    {
        private SlummingContext _context;
        public StateRepository (SlummingContext context)
            : base(context)
        {
            _context = context;
        }
        public override State GetSingle (int id)
        {
            return _context.Set<State>().FirstOrDefault(x => x.Id == id);
        }

        public override async Task<State> GetSingleAsync (int id)
        {
            return await _context.Set<State>().FirstOrDefaultAsync(e => e.Id == id);
        }
    }

}
