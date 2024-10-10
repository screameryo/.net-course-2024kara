using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Bogus;
using Xunit;

namespace BankSystem.Tests
{
    public class EmployeeServiceTests
    {
        private Faker<Employee> _employeeFaker;
        private Faker<Account> _accountFaker;

        [Fact]
        public void AddEmployeesPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage();
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            _employeeFaker = new Faker<Employee>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(e => e.Position, f => f.Name.JobTitle())
                    .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                    .RuleFor(e => e.Department, f => f.Name.JobArea())
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            for (int i = 0; i < 1000; i++)
            {
                var employee = _employeeFaker.Generate();
                employeeService.Add(employee);
            }
        }

        [Fact]
        public void EmployeeExistingThrowPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage();
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            _employeeFaker = new Faker<Employee>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(e => e.Position, f => f.Name.JobTitle())
                    .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                    .RuleFor(e => e.Department, f => f.Name.JobArea())
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            var employee = _employeeFaker.Generate();
            employeeService.Add(employee);

            Assert.Throws<InvalidOperationException>(() => employeeService.Add(employee));
        }

        [Fact]
        public void EmployeeUnderageThrowPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage();
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            _employeeFaker = new Faker<Employee>()
                     .RuleFor(c => c.FName, f => f.Name.FirstName())
                     .RuleFor(c => c.LName, f => f.Name.LastName())
                     .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(18)))
                     .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                     .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                     .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                     .RuleFor(c => c.Address, f => f.Address.FullAddress())
                     .RuleFor(e => e.Position, f => f.Name.JobTitle())
                     .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                     .RuleFor(e => e.Department, f => f.Name.JobArea())
                     .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            var employee = _employeeFaker.Generate();

            Assert.Throws<EmployeeDataException>(() => employeeService.Add(employee));
        }

        [Fact]
        public void EmployeeNoPassportDataNoSeriesThrowPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage();
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            _employeeFaker = new Faker<Employee>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(e => e.Position, f => f.Name.JobTitle())
                    .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                    .RuleFor(e => e.Department, f => f.Name.JobArea())
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            var employee = _employeeFaker.Generate();

            Assert.Throws<EmployeeDataException>(() => employeeService.Add(employee));
        }

        [Fact]
        public void EmployeeAddAdditionalAccountPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage();
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            _employeeFaker = new Faker<Employee>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(e => e.Position, f => f.Name.JobTitle())
                    .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                    .RuleFor(e => e.Department, f => f.Name.JobArea())
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            _accountFaker = new Faker<Account>()
                    .RuleFor(p => p.NameCur, f => "USD")
                    .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                    .RuleFor(a => a.AccountNumber, f => $"222484066{f.Random.Number(8).ToString("D7")}");

            var employee = _employeeFaker.Generate();
            var account = _accountFaker.Generate();

            employeeService.Add(employee);
            employeeService.AddAccount(employee, account);
        }

        [Fact]
        public void EmployeeChangeAmountAccountPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage();
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            _employeeFaker = new Faker<Employee>()
                     .RuleFor(c => c.FName, f => f.Name.FirstName())
                     .RuleFor(c => c.LName, f => f.Name.LastName())
                     .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                     .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                     .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                     .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                     .RuleFor(c => c.Address, f => f.Address.FullAddress())
                     .RuleFor(e => e.Position, f => f.Name.JobTitle())
                     .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                     .RuleFor(e => e.Department, f => f.Name.JobArea())
                     .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            _accountFaker = new Faker<Account>()
                    .RuleFor(p => p.NameCur, f => "USD")
                    .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                    .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            var employee = _employeeFaker.Generate();
            var account = _accountFaker.Generate();

            employeeService.Add(employee);
            employeeService.AddAccount(employee, account);
            employeeService.UpdateAccount(employee, account);
        }

        [Fact]
        public void SearchEmployeeNoFilterPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage();
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            _employeeFaker = new Faker<Employee>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(e => e.Position, f => f.Name.JobTitle())
                    .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                    .RuleFor(e => e.Department, f => f.Name.JobArea())
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            for (int i = 0; i < 1000; i++)
            {
                var employee = _employeeFaker.Generate();
                employeeService.Add(employee);
            }

            Assert.Equal(10, employeeStorage.Get().Count());
        }

        [Fact]
        public void GetEmployeeFIOPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage();
            EmployeeService employeeService = new EmployeeService(employeeStorage);
            string searchFname = string.Empty;
            string searchLname = string.Empty;

            _employeeFaker = new Faker<Employee>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(e => e.Position, f => f.Name.JobTitle())
                    .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                    .RuleFor(e => e.Department, f => f.Name.JobArea())
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            for (int i = 0; i < 1000; i++)
            {
                var employee = _employeeFaker.Generate();
                employeeService.Add(employee);
                if (i == 500)
                {
                    searchFname = employee.FName;
                    searchLname = employee.LName;
                }
            }

            Assert.True(employeeService.Get(f => f.FName.Equals(searchFname)
                                            && f.LName.Equals(searchLname)).Count > 0);
        }

        [Fact]
        public void GetEmployeeBetweenDatePositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage();
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            _employeeFaker = new Faker<Employee>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(e => e.Position, f => f.Name.JobTitle())
                    .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                    .RuleFor(e => e.Department, f => f.Name.JobArea())
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            for (int i = 0; i < 1000; i++)
            {
                var employee = _employeeFaker.Generate();
                employeeService.Add(employee);
            }
            Assert.True(employeeService.Get(f => f.BDate >= new DateOnly(2000, 1, 1)
                                            && f.BDate <= new DateOnly(2024, 1, 1)).Count > 0);
        }
    }
}
