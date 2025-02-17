using CookBook.Models;
using CookBook.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // GET: Recipe/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeRepository.GetAllRecipes();
            return View(recipes);
        }

        // GET: Recipe/Favourites
        [HttpGet]
        public async Task<IActionResult> Favourites()
        {
            var favourites = await _recipeRepository.GetFavourites();
            return View(favourites);
        }

        // GET: Recipe/Details/:id
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _recipeRepository.GetRecipe(id);
            return View(recipe);
        }

        // GET: Recipe/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new RecipeDTO());
        }

        // POST: Recipe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeDTO recipe)
        {
            await _recipeRepository.AddRecipe(recipe);
            return RedirectToAction(nameof(Index));
        }

        // GET: Recipe/Edit/:id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _recipeRepository.GetRecipe(id);
            return View(recipe);
        }

        // POST: Recipe/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RecipeDTO recipe)
        {
            await _recipeRepository.UpdateRecipe(id, recipe);
            return RedirectToAction(nameof(Index));
        }

        // GET: Recipe/Delete/:id
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _recipeRepository.GetRecipe(id);
            return View(recipe);
        }

        // POST: Recipe/Delete/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, RecipeDTO recipe)
        {
            await _recipeRepository.RemoveRecipe(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Recipe/Follow/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Follow(int id)
        {
            await _recipeRepository.FollowRecipe(id);
            return RedirectToAction(nameof(Favourites));
        }

        // POST: Recipe/Unfollow/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unfollow(int id)
        {
            await _recipeRepository.UnfollowRecipe(id);
            return RedirectToAction(nameof(Favourites));
        }

        // GET: Recipe/Search?searchTerm={searchTerm}
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var filteredRecipes = await _recipeRepository.SearchRecipes(searchTerm);
            return View("Index", filteredRecipes);
        }
    }
}