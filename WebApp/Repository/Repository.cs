using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApp.Data;
using WebApp.Repository.IRepository;

namespace WebApp.Repository
{
	public class Repository<T>:IRepository<T> where T : class
	{
		private readonly ApplicationDBContext _dbContext;
		internal DbSet<T> _dbSet { get; set; }
		public Repository(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<T>();
		}
		public IEnumerable<T> GetAll(string? includeProperty = null) 
		{
			IQueryable<T> query = _dbSet;
			if (includeProperty != null)
			{
				query = query.Include(includeProperty);
			}
			return query.ToList();
		}
		public T Get(Expression<Func<T, bool>> predicate, string? includeProperty = null)
		{
			IQueryable<T> query = _dbSet;
			query = query.Where(predicate);
			if (includeProperty != null)
			{
				query = query.Include(includeProperty);
			}
			return query.FirstOrDefault();
		}
		public void Add (T entity)
		{
			_dbSet.Add(entity);
		}
		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}
		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
