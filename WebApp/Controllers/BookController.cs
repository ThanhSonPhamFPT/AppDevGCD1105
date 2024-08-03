using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
		private readonly ApplicationDBContext _dbContext;
		public BookController(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IActionResult Index()
		{
			List<Book> myList = _dbContext.Books.ToList();
			return View(myList);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Book? book)
		{

			if (ModelState.IsValid)
			{
				_dbContext.Books.Add(book);
				_dbContext.SaveChanges();
				TempData["success"] = "Book Created successfully";
				return RedirectToAction("Index");
			}
			return View();
		}
		public IActionResult Edit(int? BookId)
		{
			if (BookId == null || BookId == 0)
			{
				return NotFound();
			}
			//Book? Book = _dbContext.Categories.FirstOrDefault(c => c.Id == BookId);
			Book? Book = _dbContext.Books.Find(BookId);
			if (Book == null)
			{
				return NotFound();
			}
			return View(Book);
		}
		[HttpPost]
		public IActionResult Edit(Book? book)
		{
			
			if (ModelState.IsValid)
			{
				_dbContext.Books.Update(book);
				_dbContext.SaveChanges();
				TempData["success"] = "Book updated successfully";
				return RedirectToAction("Index");
			}
			return View();
		}
		public IActionResult Delete(int? BookId)
		{
			if (BookId == null || BookId == 0)
			{
				return NotFound();
			}
			//Book? Book = _dbContext.Categories.FirstOrDefault(c => c.Id == BookId);
			Book? Book = _dbContext.Books.Find(BookId);
			if (Book == null)
			{
				return NotFound();
			}
			return View(Book);
		}
		[HttpPost]
		public IActionResult Delete(Book? book)
		{

			_dbContext.Books.Remove(book);
			_dbContext.SaveChanges();
			TempData["success"] = "Book deleted successfully";
			return RedirectToAction("Index");

		}
	}
}
