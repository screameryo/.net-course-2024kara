using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage : IEmployeeStorage
    {

        private Dictionary<Employee, List<Account>> _employees = new Dictionary<Employee, List<Account>>();

        public Dictionary<Employee, List<Account>> Data => _employees;

        public void Add(Employee newClient)
        {
            if (!_employees.TryAdd(newClient, new List<Account>()))
            {
                throw new InvalidOperationException("Сотрудник уже существует.");
            }
        }

        public void Update(Employee client)
        {
            if (!_employees.ContainsKey(client))
            {
                throw new InvalidOperationException("Сотрудник не найден.");
            }
        }

        public void Delete(Employee client)
        {
            if (!_employees.Remove(client))
            {
                throw new InvalidOperationException("Сотрудник не найден.");
            }
        }

        public void AddAccount(Employee client, Account account)
        {
            if (_employees[client].Contains(account))
            {
                throw new InvalidOperationException("Лицевой счет уже существует.");
            }
            else
            {
                _employees[client].Add(account);
            }
        }

        public void UpdateAccount(Employee client, Account account)
        {
            if (!_employees[client].Contains(account))
            {
                throw new InvalidOperationException("Лицевой счет не найден.");
            }
        }

        public void DeleteAccount(Employee client, Account account)
        {
            if (!_employees[client].Remove(account))
            {
                throw new InvalidOperationException("Лицевой счет не найден.");
            }
        }

        public List<Employee> Get(
            Expression<Func<Employee, bool>>? filter = null,
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>>? orderBy = null,
            int page = 1,
            int pageSize = 10)
        {
            var query = _employees.Keys.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
