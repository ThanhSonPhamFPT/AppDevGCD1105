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
	}
}
