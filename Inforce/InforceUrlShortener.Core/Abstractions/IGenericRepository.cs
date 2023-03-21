using System.Collections.Generic;
using System.Threading.Tasks;

namespace InforceUrlShortener.Core.Abstractions
{
    public interface IGenericRepository<T> where T: class
    {
        Task<T> Get(string id);
        Task<List<T>> GetAllAsync();
        bool Add(T entity);
        Task<bool> Delete(string id);
        void Update(T entity);
    }
}
