using WebApp.Models;

namespace WebApp.Repository.IRepository
{
	public interface IBookRepository:IRepository<Book>
	{
		public void Update(Book book);
	}
}
