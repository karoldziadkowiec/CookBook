using Microsoft.EntityFrameworkCore;

namespace CookBook.Models
{
    public class RecipeManagerContext : DbContext
    {
        public RecipeManagerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RecipeModel> Recipes { get; set; }

    }
}
