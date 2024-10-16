﻿using System.Linq.Expressions;

namespace BankSystem.App.Interfaces
{
    public interface IStorage<T>
    {
        void Add(T item);
        void Update(Guid id, T item);
        void Delete(Guid id);
        List<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int page = 1,
            int pageSize = 10);
        T? GetById(Guid id);

    }
}
