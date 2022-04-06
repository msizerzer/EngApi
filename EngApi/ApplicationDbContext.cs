using EngApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EngApi
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public ApplicationDbContext() { }
    }

}
