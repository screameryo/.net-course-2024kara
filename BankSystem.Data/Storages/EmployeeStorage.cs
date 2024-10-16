using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage : IStorage<Employee>
    {
        private readonly BankSystemDbContext _dbContext;

        public EmployeeStorage(BankSystemDbContext bankSystemDbContext)
        {
            _dbContext = bankSystemDbContext;
        }

        public void Add(Employee newClient)
        {
            try
            {
                _dbContext.Employees.Add(newClient);
                _dbContext.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                throw new DbUpdateException("Ошибка при добавлении сотрудника.", ex);
            }
        }

        public void Update(Guid id, Employee employee)
        {
            var clientToUpdate = _dbContext.Employees.Find(id);

            if (clientToUpdate == null)
            {
                throw new InvalidOperationException("Сотрудник не найден.");
            }

            clientToUpdate.FName = employee.FName;
            clientToUpdate.LName = employee.FName;
            clientToUpdate.MName = employee.MName;
            clientToUpdate.Telephone = employee.Telephone;
            clientToUpdate.Address = employee.Address;
            clientToUpdate.PassportNumber = employee.PassportNumber;
            clientToUpdate.PassportSeries = employee.PassportSeries;
            clientToUpdate.BDate = employee.BDate;

            try
            {
                _dbContext.Employees.Update(clientToUpdate);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ошибка при обновлении сотрудника.", ex);
            }
        }

        public void Delete(Guid id)
        {
            var clientToUpdate = _dbContext.Employees.Find(id);

            if (clientToUpdate == null)
            {
                throw new KeyNotFoundException("Сотрудник не найден.");
            }

            try
            {
                _dbContext.Employees.Remove(clientToUpdate);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ошибка при удалении сотрудника.", ex);
            }
        }

        public List<Employee> Get(
            Expression<Func<Employee, bool>>? filter = null,
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>>? orderBy = null,
            int page = 1,
            int pageSize = 10)
        {
            IQueryable<Employee> query = _dbContext.Employees.AsQueryable();

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

        public Employee? GetById(Guid id)
        {
            return _dbContext.Employees.Find(id);
        }
    }
}
