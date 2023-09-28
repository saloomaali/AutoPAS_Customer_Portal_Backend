using AutoPAS_Customer_portal.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AutoPAS_Customer_portal.Data
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
        {
        }

        public DbSet<PortalUser> portalUsers { get; set; }
        
        public DbSet<UserPloicyList> userPloicyList { get; set; }
    }
}
