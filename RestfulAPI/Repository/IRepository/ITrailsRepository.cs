using RestfulAPI.Model;
using RestfulAPI.Model.Dtos;
using System.Collections.Generic;

namespace RestfulAPI.Repository.IRepository
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface ITrailsRepository
    {
        IEnumerable<Trail> GetAllTrails();
        IEnumerable<Trail> GetAllTrails(int nationalParkId);
        Trail GetTrail(int Id);
        Trail GetTrail(string name);
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(int Id);
        bool DeleteTrail(Trail trail);
        bool DeleteTrail(string name);
        bool IsTrailExist(int Id);
        bool IsTrailExist(string name);
        bool Save();
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
