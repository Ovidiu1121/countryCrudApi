using CountryCrduApi.Countries.Model;
using Microsoft.EntityFrameworkCore;

namespace CountryCrduApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Country> Countries { get; set; }
    }
}
