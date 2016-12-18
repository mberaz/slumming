using Microsoft.EntityFrameworkCore;
using Slumming.DAL.Interfaces;
using Slumming.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Slumming.DAL.Repositories
{

    public class SalesmanRepository :EntityBaseRepository<Salesman>, ISalesmanRepository
    {
        private SlummingContext _context;
        public SalesmanRepository (SlummingContext context)
            : base(context)
        {
            _context = context;
        }
        public override Salesman GetSingle (int id)
        {
            return _context.Set<Salesman>().FirstOrDefault(x => x.Id == id);
        }

        public override async Task<Salesman> GetSingleAsync (int id)
        {
            return await _context.Set<Salesman>().FirstOrDefaultAsync(e => e.Id == id);
        }
    }

}
