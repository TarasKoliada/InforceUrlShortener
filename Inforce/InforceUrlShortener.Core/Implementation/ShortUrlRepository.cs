using InforceUrlShortener.Domain.Context;
using InforceUrlShortener.Domain.Entities;

namespace InforceUrlShortener.Core.Implementation
{
    public class ShortUrlRepository : GenericRepository<ShortUrl>
    {
        public ShortUrlRepository(UrlShortenerDBContext context): base(context)
        { }
    }
}
