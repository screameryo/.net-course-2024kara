using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.Data;
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
        private BankSystemDbContext _dbContext = new BankSystemDbContext();
        private TestDataGenerator _testDataGenerator = new TestDataGenerator();

        [Fact]
        public void AddEmployeesPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage(_dbContext);
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            for (int i = 0; i < 100; i++)
            {
                var employee = _testDataGenerator.GenerateEmployee(1).First();
                employeeService.Add(employee);
            }
        }

        [Fact]
        public void EmployeeGetByIdPosiiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage(_dbContext);
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            var employee = _testDataGenerator.GenerateEmployee(1).First();

            employeeService.Add(employee);

            Assert.Equal(employee, employeeService.Get(f => f.Id == employee.Id).First());
        }

        [Fact]
        public void EmployeeNoPassportDataNoSeriesThrowPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage(_dbContext);
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            _employeeFaker = new Faker<Employee>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                    .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            var employee = _employeeFaker.Generate();

            Assert.Throws<EmployeeDataException>(() => employeeService.Add(employee));
        }

        [Fact]
        public void SearchEmployeeNoFilterPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage(_dbContext);
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            for (int i = 0; i < 100; i++)
            {
                var employee = _testDataGenerator.GenerateEmployee(1).First();
                employeeService.Add(employee);
            }

            Assert.Equal(10, employeeStorage.Get().Count());
        }

        [Fact]
        public void GetEmployeeFIOPositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage(_dbContext);
            EmployeeService employeeService = new EmployeeService(employeeStorage);
            string searchFname = string.Empty;
            string searchLname = string.Empty;

            for (int i = 0; i < 100; i++)
            {
                var employee = _testDataGenerator.GenerateEmployee(1).First();
                employeeService.Add(employee);
                if (i == 50)
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
            EmployeeStorage employeeStorage = new EmployeeStorage(_dbContext);
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            for (int i = 0; i < 100; i++)
            {
                employeeService.Add(_testDataGenerator.GenerateEmployee(1).First());
            }
            Assert.True(employeeService.Get(f => f.BDate >= new DateOnly(1990, 1, 1)
                                            && f.BDate <= new DateOnly(2024, 1, 1)).Count > 0);
        }

        [Fact]
        public void EmployeeUpdatePositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage(_dbContext);
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            var employee = _testDataGenerator.GenerateEmployee(1).First();
            var newEmployee = _testDataGenerator.GenerateEmployee(1).First();

            employeeService.Add(employee);
            employeeService.Update(employee.Id, newEmployee);
        }

        [Fact]
        public void EmployeeDeletePositiveTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage(_dbContext);
            EmployeeService employeeService = new EmployeeService(employeeStorage);

            var employee = _testDataGenerator.GenerateEmployee(1).First();

            employeeService.Add(employee);
            employeeService.Delete(employee.Id);
        }
    }
}
