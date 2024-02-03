using CookBook.Models;

namespace CookBook.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeManagerContext _context;
        public RecipeRepository(RecipeManagerContext context)
        {
            _context = context;
        }

        public RecipeModel GetRecipe(int recipeId)
            => _context.Recipes.SingleOrDefault(x => x.RecipeID == recipeId);

        public IQueryable<RecipeModel> GetAllRecipes()
        {
            return _context.Recipes.OrderBy(x => x.Name);
        }

        public IQueryable<RecipeModel> GetFavourites()
        {
            return _context.Recipes.Where(x => x.IsFollowed == true).OrderBy(x => x.Name);
        }

        public void AddRecipe(RecipeModel recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }

        public void UpdateRecipe(int recipeId, RecipeModel recipe)
        {
            var result = _context.Recipes.SingleOrDefault(x => x.RecipeID == recipeId);
            if(result != null)
            {
                result.Name = recipe.Name;
                result.Time = recipe.Time;
                result.Ingredients = recipe.Ingredients;
                result.Preparation = recipe.Preparation;
                result.IsFollowed = result.IsFollowed;

                _context.SaveChanges();
            }
        }

        public void RemoveRecipe(int recipeId)
        {
            var result = _context.Recipes.SingleOrDefault(x => x.RecipeID == recipeId);
            if (result != null)
            {
                _context.Recipes.Remove(result);
                _context.SaveChanges();
            }
        }

        public IQueryable<RecipeModel> SearchRecipes(string searchTerm)
        {
            return _context.Recipes
                .Where(r => r.Name.Contains(searchTerm) || r.Ingredients.Contains(searchTerm))
                .OrderBy(r => r.Name);
        }
    }
}
