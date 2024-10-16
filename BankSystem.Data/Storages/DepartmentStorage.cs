using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class DepartmentStoragee : IStorage<Department>
    {
        private readonly BankSystemDbContext _dbContext;

        public DepartmentStoragee(BankSystemDbContext bankSystemDbContext)
        {
            _dbContext = bankSystemDbContext;
        }

        public void Add(Department item)
        {
            try
            {
                _dbContext.Departments.Add(new Department() { Name = item.Name });
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка при добавлении отдела.", ex);
            }
        }

        public void Delete(Guid id) 
        {
            var departmentToUpdate = _dbContext.Departments.Find(id);

            if (departmentToUpdate == null)
            {
                throw new KeyNotFoundException("Отдел не найден.");
            }

            try
            {
                _dbContext.Departments.Remove(departmentToUpdate);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка при удалении отдела.", ex);
            }
        }

        public List<Department> Get(
            Expression<Func<Department, bool>>? filter = null,
            Func<IQueryable<Department>, IOrderedQueryable<Department>>? orderBy = null,
            int page = 1,
            int pageSize = 10)
        {
            var query = _dbContext.Departments.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Update(Guid id, Department item)
        {
            var departmentToUpdate = _dbContext.Departments.Find(id);

            if (departmentToUpdate == null)
            {
                throw new KeyNotFoundException("Отдел не найден.");
            }

            departmentToUpdate.Name = item.Name;

            try
            {
                _dbContext.Departments.Update(departmentToUpdate);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка при обновлении отдела.", ex);
            }
        }

        public Department? GetById(Guid id)
        {
            return _dbContext.Departments.Find(id);
        }
    }
}
