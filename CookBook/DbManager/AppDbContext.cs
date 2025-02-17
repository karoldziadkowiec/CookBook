using CookBook.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookBook.DbManager
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
    }
}