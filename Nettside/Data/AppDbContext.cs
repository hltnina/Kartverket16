using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nettside.Models;

namespace Nettside.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        // Legg til en DbSet for GeoChanges
        public DbSet<GeoChanges> GeoChanges { get; set; }
    }
}
