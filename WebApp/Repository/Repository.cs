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
		public IEnumerable<T> GetAll() 
		{
			IQueryable<T> query = _dbSet;
			return query.ToList();
		}
		public T Get(Expression<Func<T, bool>> predicate)
		{
			IQueryable<T> query = _dbSet;
			query = query.Where(predicate);
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
