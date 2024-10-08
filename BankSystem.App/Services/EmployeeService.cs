﻿using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace BankSystem.App.Services
{
    public class EmployeeService : IEmployeeStorage
    {
        private readonly IEmployeeStorage _employeeStorage;

        public Dictionary<Employee, List<Account>> Data => _employeeStorage.Data;

        public EmployeeService(IEmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage ?? throw new ArgumentNullException(nameof(employeeStorage), "Хранилище сотрудников не может быть null.");
        }

        public void Add(Employee employee)
        {
            ValidateEmployee(employee);
            _employeeStorage.Add(employee);
        }

        public void Update(Employee employee)
        {
            ValidateEmployee(employee);
            _employeeStorage.Update(employee);
        }

        public void Delete(Employee employee)
        {
            _employeeStorage.Delete(employee);
        }

        public void AddEmployee(Employee employee)
        {
            ValidateEmployee(employee);
            _employeeStorage.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            ValidateEmployee(employee);
            _employeeStorage.Update(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            _employeeStorage.Delete(employee);
        }

        public void AddAccount(Employee employee, Account account)
        {
            ValidateEmployee(employee);
            ValidateAccount(account);
            _employeeStorage.AddAccount(employee, account);
        }

        public void UpdateAccount(Employee employee, Account account)
        {
            ValidateEmployee(employee);
            ValidateAccount(account);
            _employeeStorage.UpdateAccount(employee, account);
        }

        public void DeleteAccount(Employee employee, Account account)
        {
            _employeeStorage.DeleteAccount(employee, account);
        }

        public List<Employee> Get(
            Expression<Func<Employee, bool>> filter = null,
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null,
            int page = 1,
            int pageSize = 10)
        {
            return _employeeStorage.Get(filter, orderBy, page, pageSize);
        }

        private void ValidateEmployee(Employee employee)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(employee, new ValidationContext(employee), validationResults, true))
            {
                throw new EmployeeDataException($"Неверные данные: {string.Join(", ", validationResults.Select(r => r.ErrorMessage))}");
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

            if (string.IsNullOrEmpty(employee.PassportNumber?.Trim()) || string.IsNullOrEmpty(employee.PassportSeries?.Trim()))
            {
                throw new EmployeeDataException("У сотрудника отсутствуют паспортные данные.");
            }
        }

        private void ValidateAccount(Account account)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(account, new ValidationContext(account), validationResults, true))
            {
                throw new AccountDataException($"Неверные данные лицевого счета: {string.Join(", ", validationResults.Select(r => r.ErrorMessage))}");
            }

            if (account is null)
            {
                throw new ArgumentNullException(nameof(account), "Лицевой счет не может быть null.");
            }

            if (string.IsNullOrEmpty(account.AccountNumber?.Trim()))
            {
                throw new AccountDataException("Номер лицевого счета не может быть пустым.");
            }

            if (account.Amount < 0)
            {
                throw new AccountDataException("Сумма на лицевом счете не может быть отрицательной.");
            }
        }
    }
}
