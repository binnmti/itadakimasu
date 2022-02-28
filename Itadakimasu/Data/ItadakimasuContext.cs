using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Itadakimasu.Data
{
    public class ItadakimasuContext : IdentityDbContext<IdentityUser>
    {
        public ItadakimasuContext(
            DbContextOptions options) : base(options)
        {
        }

        public DbSet<FoodImage> FoodImage { get; set; } = null!;
    }
}
