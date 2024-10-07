using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public enum EmployeeMethod
    {
        Younger,
        Older
    }
    public class EmployeeStorage
    {
        private Dictionary<Employee, List<Account>> _employees = new Dictionary<Employee, List<Account>>();

        public void AddEmployee(Employee newEmployee, List<Account> newAccount)
        {
            if (!_employees.TryAdd(newEmployee, newAccount))
            {
                throw new InvalidOperationException("Клиент уже существует.");
            }
        }

        public Employee Get(EmployeeMethod method)
        {
            if (_employees is null)
            {
                throw new ArgumentNullException(nameof(_employees), "Список клиентов не может быть null.");
            }

            switch (method)
            {
                case EmployeeMethod.Younger:
                    return _employees.Keys.MinBy(c => c.BDate);
                case EmployeeMethod.Older:
                    return _employees.Keys.MaxBy(c => c.BDate);
                default:
                    throw new InvalidOperationException("Неверный метод.");
            }
        }

        public int GetAgeAverage()
        {
            if (_employees is null)
            {
                throw new ArgumentNullException(nameof(_employees), "Список клиентов не может быть null.");
            }

            return (int)_employees.Keys.Average(c => DateTime.Now.Year - c.BDate.Year);
        }

        public void AddAccountToEmployee(Employee employee, Account account)
        {
            if (_employees[employee].Contains(account))
            {
                throw new InvalidOperationException("Лицевой счет уже существует.");
            }
            else
            {
                _employees[employee].Add(account);
            }
        }

        public void RemoveAccountFromEmployee(Employee employee, Account account)
        {
            _employees[employee].Remove(account);
        }

        public void UpdateAccount(Employee employee, Account account, int newAmount)
        {
            _employees[employee].Where(a => a == account).First().Amount = newAmount;
        }

        public bool ContainsEmployee(Employee employee)
        {
            return _employees.ContainsKey(employee) ? true : false;
        }

        public Dictionary<Employee, List<Account>> SearchEmployee(string fio = "", string phone = "", string passport = "", DateOnly? dateFrom = null, DateOnly? dateTo = null)
        {
            if (string.IsNullOrEmpty(fio) && string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(passport) && dateFrom == null && dateTo == null)
            {
                return _employees;
            }

            Expression<Func<Employee, bool>> filter = c =>
                (string.IsNullOrEmpty(fio) || c.GetFullName().StartsWith(fio) || c.GetFullName().EndsWith(fio) || c.GetFullName().IndexOf(fio) >= 0) &&
                (string.IsNullOrEmpty(phone) || c.Telephone.StartsWith(phone) || c.Telephone.EndsWith(phone) || c.Telephone.IndexOf(phone) >= 0) &&
                (string.IsNullOrEmpty(passport) || c.PassportNumber.StartsWith(passport) || c.PassportNumber.EndsWith(passport) || c.PassportNumber.IndexOf(passport) >= 0) &&
                (!dateFrom.HasValue || c.BDate >= dateFrom) &&
                (!dateTo.HasValue || c.BDate <= dateTo);

            return _employees.Where(c => filter.Compile()(c.Key)).ToDictionary(c => c.Key, c => c.Value);
        }
    }
}
