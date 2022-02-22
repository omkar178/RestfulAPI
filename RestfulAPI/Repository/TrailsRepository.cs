using Microsoft.EntityFrameworkCore;
using RestfulAPI.Data;
using RestfulAPI.Model;
using RestfulAPI.Model.Dtos;
using RestfulAPI.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace RestfulAPI.Repository
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrailsRepository : ITrailsRepository
    {
        private readonly ApplicationDbContext _db;
        public TrailsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateTrail(Trail trail)
        {
            _db.trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(int Id)
        {
            var trail = _db.trails.FirstOrDefault(x => x.Id == Id);
            if (trail != null)
            {
                _db.trails.Remove(trail);
            }
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _db.trails.Remove(trail);
            return Save();
        }

        public bool DeleteTrail(string name)
        {
            var trail = _db.trails.FirstOrDefault(t => t.Name == name);
            if (trail != null)            
                _db.trails.Remove(trail);
            return Save();
            
        }

        public IEnumerable<Trail> GetAllTrails()
        {
            return _db.trails.Include(n => n.NationalPark).OrderBy(t => t.Name).ToList();
            
        }

        public IEnumerable<Trail> GetAllTrails(int nationalParkId)
        {
            return _db.trails.Include(n => n.NationalPark).Where(t => t.NationalParkId == nationalParkId).ToList();
        }

        public Trail GetTrail(int Id)
        {
            return _db.trails.Include(n => n.NationalPark).FirstOrDefault(t => t.Id == Id);
        }

        public Trail GetTrail(string name)
        {
            return _db.trails.Include(n => n.NationalPark).FirstOrDefault(t => t.Name == name);
        }

        public bool IsTrailExist(int Id)
        {
            return _db.trails.Any(n => n.Id == Id);
        }

        public bool IsTrailExist(string name)
        {
            return _db.trails.Any(n => n.Name == name);
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateTrail(Trail trail)
        {
            _db.trails.Update(trail);
            return Save();
        }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
