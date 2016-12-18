using Microsoft.EntityFrameworkCore;
using Slumming.DAL.Interfaces;
using Slumming.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Slumming.DAL.Repositories
{

    public class DealRepository : EntityBaseRepository<Deal>, IDealRepository
    {
        private SlummingContext _context;
        public DealRepository(SlummingContext context)
            : base(context)
        {
            _context = context;
        }
        public override Deal GetSingle (int id)
        {
            return _context.Set<Deal>().FirstOrDefault(x => x.Id == id);
        }

        public override async Task<Deal> GetSingleAsync (int id)
        {
            return await _context.Set<Deal>().FirstOrDefaultAsync(e => e.Id == id);
        }
    }

}
