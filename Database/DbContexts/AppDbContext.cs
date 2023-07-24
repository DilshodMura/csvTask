using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.DbContexts
{
    public class AppDbContext :DbContext
    {
        public DbSet<UserDb> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
