using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private readonly EmployeeStorage _employeeStorage;
        private readonly string KUBBank = "66";

        public EmployeeService(EmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public void AddEmployees(Employee newEmployee)
        {
            if (_employeeStorage.GetEmployees().ContainsKey(newEmployee))
            {
                throw new ClientDataExceptions(ClientDataExceptions.ClientAlreadyExistsMessage);
            }

            Account newAccount = CreateAccountFornewEmployee();

            if (ValidateEmployee(newEmployee))
            {
                _employeeStorage.AddEmployee(newEmployee, new Dictionary<string, Account> { { newAccount.AccountNumber, newAccount } });
            }
        }

        public void AddAccountToExistingClient(Employee employee, Account account)
        {
            if (ValidateEmployee(employee))
            {
                AddAccountToEmployee(employee, account);
            }
        }

        public void AddAccountToEmployee(Employee employee, Account account)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Сотрудник не может быть null.");
            }

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Лицевой счет не может быть null.");
            }

            if (_employeeStorage.GetEmployees().ContainsKey(employee))
            {
                _employeeStorage.AddAccountToEmployee(employee, account);
            }
            else
            {
                throw new EmployeeDataExceptions(EmployeeDataExceptions.NoEmployeeMessage);
            }
        }

        private bool ValidateEmployee(Employee client)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - client.BDate.Year;

            if (client.BDate > today.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                throw new ClientDataExceptions(ClientDataExceptions.UnderageClientMessage);
            }

            if (client.PassportNumber is null || client.PassportSeries is null)
            {
                throw new ClientDataExceptions(ClientDataExceptions.NoPassportDataMessage);
            }

            if (string.IsNullOrEmpty(client.PassportNumber.Trim()) || string.IsNullOrEmpty(client.PassportSeries.Trim()))
            {
                throw new ClientDataExceptions(ClientDataExceptions.NoPassportDataMessage);
            }

            return true;
        }

        private Account CreateAccountFornewEmployee()
        {
            Currency currency = new Currency { Name = "US Dollar", NumCode = "840", Symbol = "$" };
            var account = new Account
            {
                Cur = currency,
                Amount = 0,
                AccountNumber = $"2224{currency.NumCode}{KUBBank}{GenerateUniqueRandomAcc(currency.NumCode)}"
            };

            return account;
        }

        private string GenerateUniqueRandomAcc(string numcode)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 10000000);
            var employees = _employeeStorage.GetEmployees();

            while (employees.Values.Any(c => c.Keys.Any(a => a.Substring(6) == $"{numcode}{randomNumber}")))
            {
                randomNumber = random.Next(0, 10000000);
            }

            return randomNumber.ToString("D7");
        }

        public Account CreateAccountForExistingClient(string numcode)
        {
            Currency currency = new Currency { Name = "US Dollar", NumCode = numcode, Symbol = "$" };
            var account = new Account
            {
                Cur = currency,
                Amount = 0,
                AccountNumber = $"2224{currency.NumCode}{KUBBank}{GenerateUniqueRandomAcc(currency.NumCode)}"
            };

            return account;
        }

        public void UpdateAccount(Employee employee, Account account, int newAmount)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Сотрудник не может быть null.");
            }

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Лицевой счет не может быть null.");
            }

            if (newAmount < 0)
            {
                throw new AccountDataExceptions(AccountDataExceptions.AccountBalanceLessThanZeroMessage);
            }

            if (_employeeStorage.GetEmployees().ContainsKey(employee))
            {
                _employeeStorage.UpdateAccount(employee, account, newAmount);
            }
            else
            {
                throw new ClientDataExceptions(ClientDataExceptions.NoClientMessage);
            }
        }

        public Dictionary<Employee, Dictionary<string, Account>> GetEmployees(string fio = "", string phone = "", string passportSeris = "", string passportNumber = "", DateOnly? dateFrom = null, DateOnly? dateTo = null)
        {
            var employees = _employeeStorage.GetEmployees();

            if (string.IsNullOrEmpty(fio) 
                && string.IsNullOrEmpty(phone) 
                && string.IsNullOrEmpty(passportSeris) 
                && string.IsNullOrEmpty(passportNumber)
                && dateFrom == null && dateTo == null)
            {
                return employees;
            }

            Expression<Func<Employee, bool>> filter = c =>
                (string.IsNullOrEmpty(fio) || c.GetFullName().Contains(fio)) &&
                (string.IsNullOrEmpty(phone) || c.Telephone.Contains(phone)) &&
                (string.IsNullOrEmpty(passportSeris) || c.PassportSeries.Contains(passportSeris)) &&
                (string.IsNullOrEmpty(passportNumber) || c.PassportNumber.Contains(passportNumber)) &&
                (!dateFrom.HasValue || c.BDate >= dateFrom) &&
                (!dateTo.HasValue || c.BDate <= dateTo);

            return employees.Where(c => filter.Compile()(c.Key)).ToDictionary(c => c.Key, c => c.Value);
        }
    }
}
