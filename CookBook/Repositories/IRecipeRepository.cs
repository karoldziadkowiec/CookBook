using CookBook.Models;

namespace CookBook.Repositories
{
    public interface IRecipeRepository
    {
        RecipeModel GetRecipe(int recipeId);
        IQueryable<RecipeModel> GetAllRecipes();
        IQueryable<RecipeModel> GetFavourites();
        void AddRecipe(RecipeModel recipe);
        void UpdateRecipe(int recipeId, RecipeModel recipe);
        void RemoveRecipe(int recipeId);
        void FollowRecipe(int recipeId);
        void UnfollowRecipe(int recipeId);
        IQueryable<RecipeModel> SearchRecipes(string searchTerm);
    }
}