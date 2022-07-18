using Itadakimasu;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ItadakimasuWeb.Data
{
    public class ItadakimasuWebContext : IdentityDbContext<IdentityUser>
    {
        public ItadakimasuWebContext(
            DbContextOptions options) : base(options)
        {
        }

        public DbSet<FoodImage> FoodImage { get; set; } = null!;
    }
}
