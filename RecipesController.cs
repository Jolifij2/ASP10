using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipesApp.Data;
using RecipesApp.Models;

namespace RecipesApp.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recipes.ToListAsync());
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null) return NotFound();

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cuisine,CookingTime,Difficulty,Description")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();

            return View(recipe);
        }

        // POST: Recipes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cuisine,CookingTime,Difficulty,Description")] Recipe recipe)
        {
            if (id != recipe.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null) return NotFound();

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // AJAX: удаление без перезагрузки
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return NotFound(new { message = "Рецепт не найден" });

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = $"Рецепт \"{recipe.Name}\" удалён!", count = await _context.Recipes.CountAsync() });
        }

        // AJAX: поиск
        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var recipes = string.IsNullOrWhiteSpace(query)
                ? await _context.Recipes.ToListAsync()
                : await _context.Recipes
                    .Where(r => r.Name.Contains(query) || r.Cuisine.Contains(query))
                    .ToListAsync();

            return Json(recipes);
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
