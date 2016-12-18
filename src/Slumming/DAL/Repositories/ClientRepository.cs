using Microsoft.EntityFrameworkCore;
using Slumming.DAL.Interfaces;
using Slumming.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Slumming.DAL.Repositories
{

    public class ClientRepository :EntityBaseRepository<Client>, IClientRepository
    {
        private SlummingContext _context;
        public ClientRepository (SlummingContext context)
            : base(context)
        {
            _context = context;
        }
        public override Client GetSingle (int id)
        {
            return _context.Set<Client>().FirstOrDefault(x => x.Id == id);
        }

        public override async Task<Client> GetSingleAsync (int id)
        {
            return await _context.Set<Client>().FirstOrDefaultAsync(e => e.Id == id);
        }
    }

}
