using Microsoft.EntityFrameworkCore;

namespace itadakimasu.Models
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
