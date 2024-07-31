using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Category> myList = _dbContext.Categories.ToList();
            return View(myList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name.Equals(category.Description))
            {
                ModelState.AddModelError("Description", "Name can not be the same as description");
            }
            if (ModelState.IsValid)
            {
				_dbContext.Categories.Add(category);
				_dbContext.SaveChanges();
				TempData["success"] = "Category Created successfully";
				return RedirectToAction("Index");
			}
            return View();
        }
        public IActionResult Edit(int? categoryId)
        {
            if (categoryId == null || categoryId ==0)
            {
                return NotFound();
            }
            //Category? category = _dbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
            Category? category = _dbContext.Categories.Find(categoryId);
            if (category == null)
            {
                return NotFound();
            }
			return View(category);
		}
		[HttpPost]
		public IActionResult Edit(Category category)
		{
			if (category.Name.Equals(category.Description))
			{
				ModelState.AddModelError("Description", "Name can not be the same as description");
			}
			if (ModelState.IsValid)
			{
				_dbContext.Categories.Update(category);
				_dbContext.SaveChanges();
				TempData["success"] = "Category updated successfully";
				return RedirectToAction("Index");
			}
			return View();
		}
		public IActionResult Delete(int? categoryId)
		{
			if (categoryId == null || categoryId == 0)
			{
				return NotFound();
			}
			//Category? category = _dbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
			Category? category = _dbContext.Categories.Find(categoryId);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}
		[HttpPost]
		public IActionResult Delete(Category category)
		{

			_dbContext.Categories.Remove(category);
			_dbContext.SaveChanges();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");

		}
	}
}
