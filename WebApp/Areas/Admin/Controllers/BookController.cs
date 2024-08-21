using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApp.Data;
using WebApp.Models;	
using WebApp.Repository.IRepository;

namespace WebApp.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin")]
    public class BookController : Controller
    {
		private readonly IBookRepository _bookRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public BookController(IBookRepository bookRepository,IWebHostEnvironment webHostEnvironment, ICategoryRepository categoryRepository)
		{
			_bookRepository = bookRepository;
			_webHostEnvironment = webHostEnvironment;
			_categoryRepository = categoryRepository;
		}

		public IActionResult Index()
		{
			List<Book> myList = _bookRepository.GetAll("Category").ToList();
			return View(myList);
		}
		public IActionResult Create()
		{
			BookVM bookVM = new BookVM()
			{
				Categories = _categoryRepository.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				}),
				Book = new Book()
			};
			return View(bookVM);
		}
		[HttpPost]
		public IActionResult Create(Book? book, IFormFile? file)
		{

			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string bookPath = Path.Combine(wwwRootPath, @"img\books");
					using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					book.ImageUrl = @"\img\books\" + fileName;
				}
				_bookRepository.Add(book);
				_bookRepository.Save();
				TempData["success"] = "Book Created successfully";
				return RedirectToAction("Index");
			}

			BookVM bookVM = new BookVM()
			{
				Categories = _categoryRepository.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				}),
				Book = new Book()
			};
			return View(bookVM);
		}
		public IActionResult Edit(int? BookId)
		{
			BookVM bookVM = new BookVM()
			{
				Categories = _categoryRepository.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				}),
				Book = new Book()
			};
			if (BookId == null || BookId == 0)
			{
				return NotFound();
			}
			//Book? Book = _dbContext.Categories.FirstOrDefault(c => c.Id == BookId);
			bookVM.Book = _bookRepository.Get(c => c.Id == BookId);
			if (bookVM.Book == null)
			{
				return NotFound();
			}
			return View(bookVM);
		}
		[HttpPost]
		public IActionResult Edit(Book? book, IFormFile? file)
		{
			
			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string bookPath = Path.Combine(wwwRootPath, @"img\books");
					if (!string.IsNullOrEmpty(book.ImageUrl))
					{
						var oldImagePath = Path.Combine(wwwRootPath,book.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}

					using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					book.ImageUrl = @"\img\books\" + fileName;
				}
					_bookRepository.Update(book);
				_bookRepository.Save();
				TempData["success"] = "Book updated successfully";
				return RedirectToAction("Index");
			}
			BookVM bookVM = new BookVM()
			{
				Categories = _categoryRepository.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				}),
				Book = _bookRepository.Get(c => c.Id == book.Id)
			};
			return View(bookVM);
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
