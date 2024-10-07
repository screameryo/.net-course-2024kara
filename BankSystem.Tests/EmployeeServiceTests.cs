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
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8)); ;

            for (int i = 0; i < 1000; i++)
            {
                var employee = _employeeFaker.Generate();
                employeeService.AddEmployee(employee);
            }
        }

        [Fact]
        public void EmployeeAddAdditionalAccountPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage();
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            _employeeFaker = new Faker<Employee>()
                    .RuleFor(e => e.FName, f => f.Name.FirstName())
                    .RuleFor(e => e.LName, f => f.Name.LastName())
                    .RuleFor(e => e.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(e => e.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(e => e.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(e => e.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(e => e.Address, f => f.Address.FullAddress())
                    .RuleFor(e => e.Position, f => f.Name.JobTitle())
                    .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                    .RuleFor(e => e.Department, f => f.Name.JobArea())
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            _accountFaker = new Faker<Account>()
                    .RuleFor(a => a.Cur, f => new Currency())
                    .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                    .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            var employee = _employeeFaker.Generate();
            var account = _accountFaker.Generate();

            employeeService.AddEmployee(employee);
            employeeService.AddAdditionalAccount(employee, account);
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
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8)); ;

            var employee = _employeeFaker.Generate();
            employeeService.AddEmployee(employee);

            Assert.Throws<InvalidOperationException>(() => employeeService.AddEmployee(employee));
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
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8)); ;

            var employee = _employeeFaker.Generate();

            Assert.Throws<EmployeeDataException>(() => employeeService.AddEmployee(employee));
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
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(e => e.Position, f => f.Name.JobTitle())
                    .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                    .RuleFor(e => e.Department, f => f.Name.JobArea())
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8)); ;

            var employee = _employeeFaker.Generate();

            Assert.Throws<EmployeeDataException>(() => employeeService.AddEmployee(employee));
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
                    .RuleFor(a => a.Cur, f => new Currency())
                    .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                    .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            var employee = _employeeFaker.Generate();
            var account = _accountFaker.Generate();

            employeeService.AddEmployee(employee);
            employeeService.AddAdditionalAccount(employee, account);
            employeeService.UpdateAccount(employee, account, 1000);
        }

        [Fact]
        public void EmployeeChangeAmountAccountLessThanZeroThrowPositiveTest()
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
                    .RuleFor(a => a.Cur, f => new Currency())
                    .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                    .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            var employee = _employeeFaker.Generate();
            var account = _accountFaker.Generate();

            employeeService.AddEmployee(employee);
            employeeService.AddAdditionalAccount(employee, account);

            Assert.Throws<AccountDataException>(() => employeeService.UpdateAccount(employee, account, -1000));
        }

        [Fact]
        public void GetEmployeesNoFilterPositiveTest()
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
                employeeService.AddEmployee(employee);
            }

            Assert.Equal(1000, employeeStorage.SearchEmployee().Count());
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

            for(int i = 0; i < 1000; i++)
            {
                var employee = _employeeFaker.Generate();
                employeeService.AddEmployee(employee);
                if (i == 500)
                {
                    searchFname = employee.FName;
                    searchLname = employee.LName;
                }
            }

            Assert.True(employeeService.SearchEmployee(fio: $"{searchFname} {searchLname}").Count > 0);
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
                employeeService.AddEmployee(employee);
            }

            var rand = new Random().Next(0, 1000);

            Assert.True(employeeService.SearchEmployee(dateFrom: new DateOnly(1980, 1, 1), dateTo: new DateOnly(2000, 1, 1)).Count > 0);
        }
    }
}
