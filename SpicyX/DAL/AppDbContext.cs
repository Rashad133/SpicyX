using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpicyX.Models;

namespace SpicyX.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }

}
