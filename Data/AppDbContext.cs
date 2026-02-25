using Microsoft.EntityFrameworkCore;
using StoreFlow.Models;

namespace StoreFlow.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<MissingRecord> MissingRecords => Set<MissingRecord>();
    }
}
