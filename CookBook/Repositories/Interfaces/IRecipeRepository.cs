using CookBook.Models;

namespace CookBook.Repositories.Interfaces
{
    public interface IRecipeRepository
    {
        Task<RecipeDTO> GetRecipe(int recipeId);
        Task<IEnumerable<RecipeDTO>> GetAllRecipes();
        Task<IEnumerable<RecipeDTO>> GetFavourites();
        Task AddRecipe(RecipeDTO recipeDto);
        Task UpdateRecipe(int recipeId, RecipeDTO recipeDto);
        Task RemoveRecipe(int recipeId);
        Task FollowRecipe(int recipeId);
        Task UnfollowRecipe(int recipeId);
        Task<IEnumerable<RecipeDTO>> SearchRecipes(string searchTerm);
    }
}