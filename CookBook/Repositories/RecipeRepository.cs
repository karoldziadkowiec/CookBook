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
            => _context.Recipes.SingleOrDefault(x => x.recipeId == recipeId);

        public IQueryable<RecipeModel> GetAllActive()
        {
            return _context.Recipes;
        }

        public void add(RecipeModel recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }

        public void update(int recipeId, RecipeModel recipe)
        {
            var result = _context.Recipes.SingleOrDefault(x => x.recipeId == recipeId);
            if(result != null)
            {
                result.name = recipe.name;
                result.time = recipe.time;
                result.ingredients = recipe.ingredients;
                result.preparation = recipe.preparation;

                _context.SaveChanges();
            }
        }

        public void delete(int recipeId)
        {
            var result = _context.Recipes.SingleOrDefault(x => x.recipeId == recipeId);
            if (result != null)
            {
                _context.Recipes.Remove(result);
                _context.SaveChanges();
            }
        }

    }
}
