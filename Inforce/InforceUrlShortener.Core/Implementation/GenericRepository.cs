using InforceUrlShortener.Core.Abstractions;
using InforceUrlShortener.Domain.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InforceUrlShortener.Core.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UrlShortenerDBContext _context;
        public GenericRepository(UrlShortenerDBContext context)
        {
            _context = context;
        }
        public bool Add(T entity)
        {
            if (entity != null)
            {
                _context.Set<T>().Add(entity);
                return true;
            }
            return false;
        }

        public virtual async Task<bool> Delete(string id)
        {
            var entity = await Get(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                return true;
            }
            return false;
        }

        public async Task<T> Get(string id) => await _context.Set<T>().FindAsync(id);

        public async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
