using WebApp.Data;
using WebApp.Models;
using WebApp.Repository.IRepository;

namespace WebApp.Repository
{
	public class CategoryRepository:Repository<Category>,ICategoryRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public CategoryRepository(ApplicationDBContext dbContext):base(dbContext) { _dbContext = dbContext; }
		public void Update(Category category)
		{
			_dbContext.Categories.Update(category);
		}

	}
}
