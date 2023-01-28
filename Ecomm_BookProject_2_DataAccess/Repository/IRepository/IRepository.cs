using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_BookProject_2_DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        void Add(T entity); // Database data adding code
        void Remove(T entity); // Database data Remove code
        void Remove(int id); // Database data Remove code by id
        void RemoveRange(IEnumerable<T> entity);
        T Get(int id);  // Find code
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null // include table code for example (Category,CoverType)
            );
        T FirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null // include table code for example (Category,CoverType)
            );
    }
}
