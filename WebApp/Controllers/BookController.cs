using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;	
using WebApp.Repository.IRepository;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
		private readonly IBookRepository _bookRepository;
		public BookController(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public IActionResult Index()
		{
			List<Book> myList = _bookRepository.GetAll().ToList();
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
				_bookRepository.Add(book);
				_bookRepository.Save();
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
			Book? Book = _bookRepository.Get(c => c.Id == BookId);
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
				_bookRepository.Update(book);
				_bookRepository.Save();
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
			Book? Book = _bookRepository.Get(c => c.Id == BookId);
			if (Book == null)
			{
				return NotFound();
			}
			return View(Book);
		}
		[HttpPost]
		public IActionResult Delete(Book? book)
		{

			_bookRepository.Delete(book);
			_bookRepository.Save();
			TempData["success"] = "Book deleted successfully";
			return RedirectToAction("Index");

		}
	}
}
