using NationalParkWeb.Repository.IRepository;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;

namespace NationalParkWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly IHttpClientFactory _httpClientFactory;

        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateAsync(string URL, T ObjCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, URL);

            if (ObjCreate != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(ObjCreate),Encoding.UTF8,"application/json");
            else
                return false;

            HttpResponseMessage response = await _httpClientFactory.CreateClient().SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Created)
                return true;
            else
                return false;
            
        }

        public async Task<bool> DeleteAsync(string URL,int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, URL + Id);
            HttpResponseMessage response = await _httpClientFactory.CreateClient().SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Created)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string URL)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,URL);
            HttpResponseMessage response = await _httpClientFactory.CreateClient().SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            { 
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            return null;
        }

        public async Task<T> GetAsync(string URL, int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, URL + Id);
            HttpResponseMessage response = await _httpClientFactory.CreateClient().SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);

            }
            return null;
        }

        public async Task<bool> UpdateAsync(string URL, T ObjUpdate)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, URL);
            if (ObjUpdate != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(ObjUpdate), Encoding.UTF8, "application/json");
            else
                return false;

            HttpResponseMessage response = await _httpClientFactory.CreateClient().SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NoContent)
                return true;
            else
                return false;
        }
    }
}
