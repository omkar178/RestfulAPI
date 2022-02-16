using RestfulAPI.Data;
using RestfulAPI.Model;
using RestfulAPI.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace RestfulAPI.Repository
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NationalParkRepository : INationalParkRepository

    {
        public readonly ApplicationDbContext _db;
        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.nationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.nationalParks.Remove(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(int Id)
        {
            var _nationalPark = _db.nationalParks.FirstOrDefault(n => n.Id == Id);
            if (_nationalPark != null)
            {
                _db.nationalParks.Remove(_nationalPark);
            }
            return Save();
        }

        public IEnumerable<NationalPark> GetAllNationalParks()
        {
            return _db.nationalParks.OrderBy(n => n.Name).ToList();
        }

        public NationalPark GetNationalPark(int Id)
        {
            return _db.nationalParks.FirstOrDefault(n => n.Id == Id);
        }

        public NationalPark GetNationalPark(string name)
        {
            return _db.nationalParks.FirstOrDefault(n => n.Name == name);
        }

        public bool IsExist(int Id)
        {
            return _db.nationalParks.Any(n => n.Id == Id);
        }

        public bool IsExist(string Name)
        {
            return _db.nationalParks.Any(n => n.Name == Name);
        }

        public bool Save()
        {
           return _db.SaveChanges() > 0?true:false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
           _db.nationalParks.Update(nationalPark);
            return Save();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
