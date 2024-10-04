using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public enum EmployeeMethod
    {
        Younger,
        Older,
        Last
    }
    public class EmployeeStorage
    {
        private Dictionary<Employee, Dictionary<string, Account>> _employees = new Dictionary<Employee, Dictionary<string, Account>>();

        public void AddEmployee(Employee newEmployee, Dictionary<string, Account> newAccount)
        {
            _employees.Add(newEmployee, newAccount);
        }

        public Dictionary<Employee, Dictionary<string, Account>> GetEmployees()
        {
            return _employees;
        }

        public Employee Get(EmployeeMethod method)
        {
            switch (method)
            {
                case EmployeeMethod.Younger:
                    return _employees.Keys.OrderBy(c => c.BDate).First();
                case EmployeeMethod.Older:
                    return _employees.Keys.OrderByDescending(c => c.BDate).First();
                case EmployeeMethod.Last:
                    return _employees.Keys.Last();
                default:
                    throw new InvalidOperationException("Неверный метод.");
            }
        }

        public int GetAgeAverage()
        {
            return (int)_employees.Keys.Average(c => DateTime.Now.Year - c.BDate.Year);
        }

        public void AddAccountToEmployee(Employee employee, Account account)
        {
            _employees[employee].Add(account.AccountNumber, account);
        }

        public void RemoveAccountFromEmployee(Employee employee, Account account)
        {
            _employees[employee].Remove(account.AccountNumber);
        }

        public void UpdateAccount(Employee employee, Account account, int newAmount)
        {
            account.Amount = (int)newAmount;
        }
    }
}
