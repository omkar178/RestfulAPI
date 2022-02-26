using NationalParkWeb.Repository.IRepository;
using System.Net.Http;
using System.Threading.Tasks;

namespace NationalParkWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly IHttpClientFactory _httpClientFactory;

        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public Task<bool> CreateAsync(string URL, T ObjCreate)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(string URL)
        {
            throw new System.NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<T>> GetAllAsync(string URL)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetAsync(string URL, int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(string URL, T ObjUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}
