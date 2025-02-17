using AutoMapper;
using CookBook.DbManager;
using CookBook.Models;
using CookBook.Models.Entities;
using CookBook.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Repositories.Classes
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public RecipeRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RecipeDTO> GetRecipe(int recipeId)
        {
            var recipe = await _context.Recipes.SingleOrDefaultAsync(r => r.RecipeID == recipeId);
            var recipeDTO = _mapper.Map<RecipeDTO>(recipe);
            return recipeDTO;
        }

        public async Task<IEnumerable<RecipeDTO>> GetAllRecipes()
        {
            var recipes = await _context.Recipes.OrderBy(r => r.Name).ToListAsync();
            var recipesDTO = _mapper.Map<IEnumerable<RecipeDTO>>(recipes);
            return recipesDTO;
        }

        public async Task<IEnumerable<RecipeDTO>> GetFavourites()
        {
            var favourites = await _context.Recipes
                .Where(r => r.IsFollowed == true)
                .OrderBy(r => r.Name)
                .ToListAsync();

            var favouritesDTO = _mapper.Map<IEnumerable<RecipeDTO>>(favourites);
            return favouritesDTO;
        }

        public async Task AddRecipe(RecipeDTO recipeDto)
        {
            var recipe = _mapper.Map<Recipe>(recipeDto);
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecipe(int recipeId, RecipeDTO recipeDto)
        {
            var result = await _context.Recipes.SingleOrDefaultAsync(r => r.RecipeID == recipeId);
            if (result != null)
            {
                result.Name = recipeDto.Name;
                result.Time = recipeDto.Time;
                result.Ingredients = recipeDto.Ingredients;
                result.Preparation = recipeDto.Preparation;
                result.IsFollowed = result.IsFollowed;

                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveRecipe(int recipeId)
        {
            var recipe = await _context.Recipes.SingleOrDefaultAsync(r => r.RecipeID == recipeId);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task FollowRecipe(int recipeId)
        {
            var recipe = await _context.Recipes.SingleOrDefaultAsync(r => r.RecipeID == recipeId);
            if (recipe != null)
            {
                recipe.IsFollowed = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnfollowRecipe(int recipeId)
        {
            var recipe = await _context.Recipes.SingleOrDefaultAsync(r => r.RecipeID == recipeId);
            if (recipe != null)
            {
                recipe.IsFollowed = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RecipeDTO>> SearchRecipes(string searchTerm)
        {
            var filteredRecipes = await _context.Recipes
                .Where(r => r.Name.Contains(searchTerm) || r.Ingredients.Contains(searchTerm))
                .OrderBy(r => r.Name)
                .ToListAsync();

            var filteredRecipesDTO = _mapper.Map<IEnumerable<RecipeDTO>>(filteredRecipes);
            return filteredRecipesDTO;
        }
    }
}