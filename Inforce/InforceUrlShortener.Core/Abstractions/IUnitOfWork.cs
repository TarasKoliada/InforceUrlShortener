using InforceUrlShortener.Core.Implementation;
using InforceUrlShortener.Domain.Entities;
using System.Threading.Tasks;

namespace InforceUrlShortener.Core.Abstractions
{
    public interface IUnitOfWork
    {
        public UserRepository Users { get; }
        public IGenericRepository<ShortUrl> ShortUrls { get; }
        public Task<int> SaveAsync();
    }
}
