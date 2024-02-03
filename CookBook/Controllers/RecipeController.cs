using CookBook.Models;
using CookBook.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // GET: Recipe
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        // GET: Recipe/Index
        [HttpGet]
        public ActionResult Index()
        {
            return View(_recipeRepository.GetAllRecipes());
        }

        // GET: Recipe/Favourites
        [HttpGet]
        public ActionResult Favourites()
        {
            return View(_recipeRepository.GetFavourites());
        }

        // GET: Recipe/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(_recipeRepository.GetRecipe(id));
        }

        // GET: Recipe/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new RecipeModel());
        }

        // POST: Recipe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeModel recipeModel)
        {
            _recipeRepository.AddRecipe(recipeModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Recipe/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_recipeRepository.GetRecipe(id));
        }

        // POST: Recipe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RecipeModel recipeModel)
        {
            _recipeRepository.UpdateRecipe(id, recipeModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Recipe/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(_recipeRepository.GetRecipe(id));
        }

        // POST: Recipe/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RecipeModel recipeModel)
        {
            _recipeRepository.RemoveRecipe(id);

            return RedirectToAction(nameof(Index));
        }

        // POST: Recipe/Follow/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Follow(int id)
        {
            var recipeModel = _recipeRepository.GetRecipe(id);

            if (recipeModel != null)
            {
                recipeModel.IsFollowed = true;
                _recipeRepository.UpdateRecipe(id, recipeModel);
            }

            return RedirectToAction(nameof(Favourites));
        }

        // POST: Recipe/Unfollow/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unfollow(int id)
        {
            var recipeModel = _recipeRepository.GetRecipe(id);

            if (recipeModel != null)
            {
                recipeModel.IsFollowed = false;
                _recipeRepository.UpdateRecipe(id, recipeModel);
            }

            return RedirectToAction(nameof(Favourites));
        }

        // GET: Recipe/Search?searchTerm={searchTerm}
        [HttpGet]
        public ActionResult Search(string searchTerm)
        {
            var searchResults = _recipeRepository.SearchRecipes(searchTerm);

            return View("Index", searchResults);
        }
    }
}