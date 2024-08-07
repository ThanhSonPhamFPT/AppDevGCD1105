using WebApp.Data;
using WebApp.Models;
using WebApp.Repository.IRepository;

namespace WebApp.Repository
{
	public class BookRepository : Repository<Book>, IBookRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public BookRepository(ApplicationDBContext dbContext) : base(dbContext) { _dbContext = dbContext; }

		public void Update(Book book)
		{
			_dbContext.Books.Update(book);
		}
	}
}
