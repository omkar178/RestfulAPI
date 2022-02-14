using RestfulAPI.Model;
using System.Collections.Generic;

namespace RestfulAPI.Repository.IRepository
{
    public interface INationalParkRepository
    {
        IEnumerable<NationalPark> GetAllNationalParks();
        NationalPark GetNationalPark(int Id);
        NationalPark GetNationalPark(string name);
        bool CreateNationalPark(NationalPark nationalPark);
        bool UpdateNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(int Id);
        bool IsExist(int Id);
        bool IsExist(string Name);
        bool Save();
        
    }
}
