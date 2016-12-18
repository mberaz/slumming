using Microsoft.EntityFrameworkCore;
using Slumming.DAL.Interfaces;
using Slumming.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Slumming.DAL.Repositories
{

    public class ApartmentRepository :EntityBaseRepository<Apartment>, IApartmentRepository
    {
        private SlummingContext _context;
        public ApartmentRepository (SlummingContext context)
            : base(context)
        {
            _context = context;
        }

        public override Apartment GetSingle (int id)
        {
            return _context.Set<Apartment>().FirstOrDefault(x => x.Id == id);
        }

        public override async Task<Apartment> GetSingleAsync (int id)
        {
            return await _context.Set<Apartment>().FirstOrDefaultAsync(e => e.Id == id);
        }
    }

}
