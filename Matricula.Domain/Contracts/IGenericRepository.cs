using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Domain.Contracts
{
    public interface IGenericRepository<T>
    {
        void Add(T entity);
        T AddAndReturn(T entity);
        void AddRange(List<T> entities);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
        void Update(T entity);
        T Find(object id);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        public T FindFirstOrDefault(Expression<Func<T, bool>> predicate);
        public IEnumerable<T> GetAll();
        public int Max(Expression<Func<T, int>> predicate);
    }
}
