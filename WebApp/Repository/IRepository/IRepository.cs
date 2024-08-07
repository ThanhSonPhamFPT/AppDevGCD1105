using System.Collections;
using System.Linq.Expressions;

namespace WebApp.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		public IEnumerable<T> GetAll();
		public T Get(Expression<Func<T, bool>> predicate);
		void Add(T entity);
		void Delete(T entity);
		void Save();
	}
}
