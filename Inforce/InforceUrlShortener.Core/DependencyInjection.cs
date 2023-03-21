using InforceUrlShortener.Core.Abstractions;
using InforceUrlShortener.Core.Implementation;
using InforceUrlShortener.Domain.Context;
using InforceUrlShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InforceUrlShortener.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<UserRepository, UserRepository>();
            services.AddTransient<IGenericRepository<ShortUrl>, ShortUrlRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UrlShortenerDBContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}
