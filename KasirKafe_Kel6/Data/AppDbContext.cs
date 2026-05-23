using Microsoft.EntityFrameworkCore;
using KasirKafe_Kel6.Models;

namespace KasirKafe_Kel6.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Menu> Menus { get; set; }
    }
}
