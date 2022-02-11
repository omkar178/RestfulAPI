using Microsoft.EntityFrameworkCore;
using RestfulAPI.Model;

namespace RestfulAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<NationalPark> nationalParks { get; set; }
    }
}
