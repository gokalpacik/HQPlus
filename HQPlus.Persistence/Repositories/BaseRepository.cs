using HQPlus.Application.Contracts.Persistence;
using HQPlus.Domain.Entity;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace HQPlus.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        public async Task<IEnumerable<TSource>> ListAllAsync<TSource>(string filePath)
        {
            using StreamReader reader = new StreamReader(filePath);
            string json = await reader.ReadToEndAsync();

            return JsonSerializer.Deserialize<IEnumerable<TSource>>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }
    }
}