using InforceUrlShortener.Core.Abstractions;
using InforceUrlShortener.Domain.Context;
using InforceUrlShortener.Domain.Entities;
using System.Threading.Tasks;

namespace InforceUrlShortener.Core.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UrlShortenerDBContext _context;

        public UserRepository Users { get; }

        public IGenericRepository<ShortUrl> ShortUrls { get; }

        public UnitOfWork(UrlShortenerDBContext context, UserRepository users, IGenericRepository<ShortUrl> shortUrls)
        {
            _context = context;
            Users = users;
            ShortUrls = shortUrls;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        
    }
}
