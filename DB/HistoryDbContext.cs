using DB.Models;
using System.Data.Entity;

namespace DB
{
    public class HistoryDbContext : DbContext
    {
        public HistoryDbContext(string connectionString) : base(connectionString)
        {
        }
        public DbSet<Record> Records { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
