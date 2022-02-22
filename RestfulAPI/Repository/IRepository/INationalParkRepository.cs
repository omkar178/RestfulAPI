using RestfulAPI.Model;
using System.Collections.Generic;

namespace RestfulAPI.Repository.IRepository
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
