using DB.Models;
using System.Data.Entity;

namespace DB
{
    public class BitcoinHistoryDbContext : DbContext
    {
        public BitcoinHistoryDbContext(string connectionString) : base(connectionString)
        {
        }
        public DbSet<Record> Records { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
