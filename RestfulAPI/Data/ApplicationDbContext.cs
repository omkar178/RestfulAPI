using Microsoft.EntityFrameworkCore;
using RestfulAPI.Model;

namespace RestfulAPI.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<NationalPark> nationalParks { get; set; }
        public DbSet<Trail> trails { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
