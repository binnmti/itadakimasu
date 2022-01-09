using Microsoft.EntityFrameworkCore;
using Models;

namespace Itadakimasu.Data
{
    public class ItadakimasuContext : DbContext
    {
        public ItadakimasuContext(DbContextOptions<ItadakimasuContext> options)
            : base(options)
        {
        }

        public DbSet<Food> Food { get; set; } = null!;
    }
}
