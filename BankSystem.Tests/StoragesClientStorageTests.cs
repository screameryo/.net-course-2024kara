using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Bogus;
using Xunit;

namespace BankSystem.Tests
{
    public class StoragesClientStorageTests
    {
        private Faker<Client> _clientFaker;
        private Faker<Employee> _employeeFaker;

        [Fact]
        public void AddClientShouldAddClientToStoragePositivTest()
        {
            var clientStorage = new ClientStorage();

            _clientFaker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            var client = _clientFaker.Generate();

            clientStorage.AddClient(client);

            Assert.Equal(client, clientStorage.clients.LastOrDefault());
        }

        [Fact]
        public void AddClientShouldThrowExceptionWhenClientIsNullPositivTest()
        {
            var clientStorage = new ClientStorage();

            Assert.Throws<ArgumentNullException>(() => clientStorage.AddClient(null));
        }

        [Fact]
        public void YoungestClientPositivTest()
        {
            var clientStorage = new ClientStorage();

            _clientFaker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            var client1 = _clientFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(1990, 5, 1))).Generate();
            var client2 = _clientFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(2000, 7, 15))).Generate();
            var client3 = _clientFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(1987, 1, 26))).Generate();

            clientStorage.AddClient(client1);
            clientStorage.AddClient(client2);
            clientStorage.AddClient(client3);

            Assert.Equal(client2, clientStorage.clients.OrderByDescending(c => c.BDate).FirstOrDefault());
        }

        [Fact]
        public void OldestClientPositivTest()
        {
            var clientStorage = new ClientStorage();

            _clientFaker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            var client1 = _clientFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(1990, 5, 1))).Generate();
            var client2 = _clientFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(2000, 7, 15))).Generate();
            var client3 = _clientFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(1987, 1, 26))).Generate();

            clientStorage.AddClient(client1);
            clientStorage.AddClient(client2);
            clientStorage.AddClient(client3);

            Assert.Equal(client3, clientStorage.clients.OrderBy(c => c.BDate).FirstOrDefault());
        }

        [Fact]
        public void AverageAgePositivTest()
        {
            var clientStorage = new ClientStorage();

            _clientFaker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            var client1 = _clientFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(1990, 5, 1))).Generate();
            var client2 = _clientFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(2000, 7, 15))).Generate();
            var client3 = _clientFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(1987, 10, 26))).Generate();

            clientStorage.AddClient(client1);
            clientStorage.AddClient(client2);
            clientStorage.AddClient(client3);

            Assert.Equal(clientStorage.AverageAge(), 
                (int)(clientStorage.clients
                .Select(c => (DateTime.Now.Year - c.BDate.Year))
                .Sum() / 3));
        }

        [Fact]
        public void AddEmployeeShouldAddClientToStoragePositivTest()
        {
            var employeesStorage = new EmployeeStorage();

            _employeeFaker = new Faker<Employee>()
                .RuleFor(e => e.FName, f => f.Name.FirstName())
                .RuleFor(e => e.LName, f => f.Name.LastName())
                .RuleFor(e => e.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(e => e.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(e => e.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                .RuleFor(e => e.Department, f => f.Name.JobArea())
                .RuleFor(e => e.Contract, f => $"Contract to {DateTime.Now.AddMonths(f.Random.Number(1, 24)).ToString("d")}");

            var employee = _employeeFaker.Generate();

            employeesStorage.AddEmployee(employee);

            Assert.Equal(employee, employeesStorage.employees.LastOrDefault());
        }

        [Fact]
        public void AddEmployeeShouldThrowExceptionWhenClientIsNullPositivTest()
        {
            var employeesStorage = new EmployeeStorage();

            Assert.Throws<ArgumentNullException>(() => employeesStorage.AddEmployee(null));
        }

        [Fact]
        public void YoungestEmployeePositivTest()
        {
            var employeeStorage = new EmployeeStorage();

            _employeeFaker = new Faker<Employee>()
                .RuleFor(e => e.FName, f => f.Name.FirstName())
                .RuleFor(e => e.LName, f => f.Name.LastName())
                .RuleFor(e => e.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(e => e.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(e => e.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                .RuleFor(e => e.Department, f => f.Name.JobArea())
                .RuleFor(e => e.Contract, f => $"Contract to {DateTime.Now.AddMonths(f.Random.Number(1, 24)).ToString("d")}");

            var employee1 = _employeeFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(1990, 5, 1))).Generate();
            var employee2 = _employeeFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(2000, 7, 15))).Generate();
            var employee3 = _employeeFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(1987, 1, 26))).Generate();

            employeeStorage.AddEmployee(employee1);
            employeeStorage.AddEmployee(employee2);
            employeeStorage.AddEmployee(employee3);

            Assert.Equal(employee2, employeeStorage.employees.OrderByDescending(c => c.BDate).FirstOrDefault());
        }

        [Fact]
        public void OldestEmployeePositivTest()
        {
            var employeeStorage = new EmployeeStorage();

            _employeeFaker = new Faker<Employee>()
                .RuleFor(e => e.FName, f => f.Name.FirstName())
                .RuleFor(e => e.LName, f => f.Name.LastName())
                .RuleFor(e => e.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(e => e.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(e => e.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                .RuleFor(e => e.Department, f => f.Name.JobArea())
                .RuleFor(e => e.Contract, f => $"Contract to {DateTime.Now.AddMonths(f.Random.Number(1, 24)).ToString("d")}");

            var employee1 = _employeeFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(1990, 5, 1))).Generate();
            var employee2 = _employeeFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(2000, 7, 15))).Generate();
            var employee3 = _employeeFaker.RuleFor(c => c.BDate, f => DateOnly.FromDateTime(new DateTime(1987, 1, 26))).Generate();

            employeeStorage.AddEmployee(employee1);
            employeeStorage.AddEmployee(employee2);
            employeeStorage.AddEmployee(employee3);

            Assert.Equal(employee3, employeeStorage.employees.OrderBy(c => c.BDate).FirstOrDefault());
        }
    }
}
