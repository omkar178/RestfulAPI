using NationalParkWeb.Models;
using NationalParkWeb.Repository.IRepository;
using System.Net.Http;

namespace NationalParkWeb.Repository
{
    public class TrailRepository : Repository<Trail>, ITrailRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TrailRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
