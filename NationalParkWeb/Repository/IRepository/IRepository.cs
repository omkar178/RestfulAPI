using System.Collections.Generic;
using System.Threading.Tasks;

namespace NationalParkWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(string URL,int? Id);
        Task<IEnumerable<T>> GetAllAsync(string URL);
        Task<bool> CreateAsync(string URL, T ObjCreate);
        Task<bool> UpdateAsync(string URL,T ObjUpdate);
        Task<bool> DeleteAsync(string URL,int Id);

    }
}
