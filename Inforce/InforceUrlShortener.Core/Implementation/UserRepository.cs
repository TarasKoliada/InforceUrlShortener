using InforceUrlShortener.Domain.Context;
using InforceUrlShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InforceUrlShortener.Core.Implementation
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly UrlShortenerDBContext _context;
        public UserRepository(UrlShortenerDBContext context): base(context)
        { _context = context;  }

        public async Task<User> GetEntityByIdAsync(string id)
        {
            Guid guidId = Guid.Parse(id);
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == guidId.ToString());
            return user;
        }      
    }
}
