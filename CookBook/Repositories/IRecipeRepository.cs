using CookBook.Models;

namespace CookBook.Repositories
{
    public interface IRecipeRepository
    {
        RecipeModel GetRecipe(int recipeId);
        IQueryable<RecipeModel> GetAllActive();
        void add(RecipeModel recipe);
        void update(int recipeId, RecipeModel recipe);
        void delete(int recipeId);
    }
}
