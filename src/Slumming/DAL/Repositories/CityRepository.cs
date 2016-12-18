using Microsoft.EntityFrameworkCore;
using Slumming.DAL.Interfaces;
using Slumming.Models;
using System.Linq;
using System.Threading.Tasks;


namespace Slumming.DAL.Repositories
{

    public class CityRepository :EntityBaseRepository<City>, ICityRepository
    {
        private SlummingContext _context;
        public CityRepository (SlummingContext context)
            : base(context)
        {
            _context = context;
        }

        public override City GetSingle (int id)
        {
            return _context.Set<City>().FirstOrDefault(x => x.Id == id);
        }

        public override async Task<City> GetSingleAsync (int id)
        {
            return await _context.Set<City>().FirstOrDefaultAsync(e => e.Id == id);
        }
    }

}
