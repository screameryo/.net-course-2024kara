using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Data.Interfaces
{
    public interface IStorage<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        List<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int page = 1,
            int pageSize = 10);

    }
}
