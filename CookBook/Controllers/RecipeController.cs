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
        // GET: Recipe
        public ActionResult Index()
        {
            return View(_recipeRepository.GetAllActive());
        }

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // GET: Recipe/Details/5
        public ActionResult Details(int id)
        {
            return View(_recipeRepository.GetRecipe(id));
        }

        // GET: Recipe/Create
        public ActionResult Create()
        {
            return View(new RecipeModel());
        }

        // POST: Recipe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeModel recipeModel)
        {
            _recipeRepository.add(recipeModel);


            return RedirectToAction(nameof(Index));
        }

        // GET: Recipe/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_recipeRepository.GetRecipe(id));
        }

        // POST: Recipe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RecipeModel recipeModel)
        {
            _recipeRepository.update(id, recipeModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Recipe/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_recipeRepository.GetRecipe(id));
        }

        // POST: Recipe/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RecipeModel recipeModel)
        {
            _recipeRepository.delete(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
