using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace backend.db
{
    public class Connection:DbContext
    {
        public DbSet<MenuModel> MenuModels { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=nested_menu;Trusted_Connection=true;TrustServerCertificate=True;Encrypt=False");
        }
    }
}
