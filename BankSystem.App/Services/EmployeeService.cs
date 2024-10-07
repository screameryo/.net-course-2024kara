using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private readonly EmployeeStorage _employeeStorage;

        public EmployeeService(EmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage ?? throw new ArgumentNullException(nameof(employeeStorage), "Хранилище сотрудников не может быть null.");
        }

        public void AddEmployee(Employee newEmployee)
        {
            Account newAccount = CreateAccount();

            if (ValidateEmployee(newEmployee))
            {
                _employeeStorage.AddEmployee(newEmployee, new List<Account> { { newAccount } });
            }
        }

        public void AddAdditionalAccount(Employee employee, Account account)
        {
            if (ValidateEmployee(employee))
            {
                if (employee is null)
                {
                    throw new ArgumentNullException(nameof(employee), "Сотрудник не может быть null.");
                }

                if (account is null)
                {
                    throw new ArgumentNullException(nameof(account), "Лицевой счет не может быть null.");
                }

                _employeeStorage.AddAccountToEmployee(employee, account);
            }
        }        

        private bool ValidateEmployee(Employee employee)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(employee, new ValidationContext(employee), validationResults, true))
            {
                throw new EmployeeDataException($"Неверные данные сотрудника {validationResults.Select(r => r.ErrorMessage).Aggregate((a, b) => $"{a}{Environment.NewLine}{b}")}");
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - employee.BDate.Year;

            if (employee.BDate > today.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                throw new EmployeeDataException("Сотрудник должен быть старше 18 лет.");
            }

            if (employee.PassportNumber is null || employee.PassportSeries is null)
            {
                throw new EmployeeDataException("У сотрудника отсутствуют паспортные данные.");
            }

            if (string.IsNullOrEmpty(employee.PassportNumber.Trim()) || string.IsNullOrEmpty(employee.PassportSeries.Trim()))
            {
                throw new EmployeeDataException("У сотрудника отсутствуют паспортные данные.");
            }

            return true;
        }

        private Account CreateAccount()
        {
            Random random = new Random();
            var account = new Account
            {
                Cur = new Currency { Name = "US Dollar", NumCode = "840", Symbol = "$" },
                Amount = 0,
                AccountNumber = $"222484066{random.Next(0, 10000000).ToString("D7")}"
            };

            return account;
        }

        public void UpdateAccount(Employee employee, Account account, int newAmount)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee), "Сотрудник не может быть null.");
            }

            if (account is null)
            {
                throw new ArgumentNullException(nameof(account), "Лицевой счет не может быть null.");
            }

            if (newAmount < 0)
            {
                throw new AccountDataException("Сумма на лицевом счете меньше 0.");
            }

            if (_employeeStorage.ContainsEmployee(employee))
            {
                _employeeStorage.UpdateAccount(employee, account, newAmount);
            }
            else
            {
                throw new ClientDataException("Сотрудник не найден.");
            }
        }

        public Dictionary<Employee, List<Account>> SearchEmployee(string fio = "", string phone = "", string passport = "", DateOnly? dateFrom = null, DateOnly? dateTo = null)
        {
            return _employeeStorage.SearchEmployee(fio, phone, passport, dateFrom, dateTo);
        }
    }
}
