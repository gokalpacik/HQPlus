using System.Collections.Generic;
using System.Threading.Tasks;

namespace HQPlus.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<IEnumerable<TSource>> ListAllAsync<TSource>(string filePath);        
    }
}
