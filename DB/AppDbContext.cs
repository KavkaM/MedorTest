using DB.Models;
using System.Data.Entity;

namespace DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string connectionString) : base(connectionString)
        {
        }
        public DbSet<Record> Records { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
