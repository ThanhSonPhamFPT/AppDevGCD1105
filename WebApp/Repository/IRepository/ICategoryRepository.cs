using WebApp.Models;

namespace WebApp.Repository.IRepository
{
	public interface ICategoryRepository:IRepository<Category>
	{
		public void Update(Category entity);
	}
}
