using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using System.Linq;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage : IStorage<Employee>
    {
        private Dictionary<Guid, Employee> _employees = new Dictionary<Guid, Employee>();

        public Dictionary<Guid, Employee> Data => _employees;

        public void Add(Employee newClient)
        {
            if (!_employees.TryAdd(newClient.Id, newClient))
            {
                throw new InvalidOperationException("Сотрудник уже существует.");
            }
        }

        public void Update(Employee client)
        {
            if (!_employees.ContainsKey(client.Id))
            {
                throw new InvalidOperationException("Сотрудник не найден.");
            }
        }

        public void Delete(Employee client)
        {
            if (!_employees.Remove(client.Id))
            {
                throw new InvalidOperationException("Сотрудник не найден.");
            }
        }

        public List<Employee> Get(
            Expression<Func<Employee, bool>>? filter = null,
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>>? orderBy = null,
            int page = 1,
            int pageSize = 10)
        {
            IQueryable<Employee> query = _employees.Values.AsQueryable();

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
    }
}
