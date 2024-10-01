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
        public void AddClientToStoragePositivTest()
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

            Assert.Contains(client, clientStorage.GetClients());
        }

        [Fact]
        public void AddClientShouldThrowExceptionWhenClientIsNullPositivTest()
        {
            var clientStorage = new ClientStorage();

            Assert.Throws<ArgumentNullException>(() => clientStorage.AddClient(null));
        }

        [Fact]
        public void YoungestClientTest()
        {
            var clientStorage = new ClientStorage();

            _clientFaker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            clientStorage.AddManyClients(_clientFaker.Generate(1000));

            Client youngestClient = clientStorage.Get(ClientMethod.Younger);
        }

        [Fact]
        public void OldestClientTest()
        {
            var clientStorage = new ClientStorage();

            _clientFaker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            clientStorage.AddManyClients(_clientFaker.Generate(1000));

            Client oldestClient = clientStorage.Get(ClientMethod.Older);
        }

        [Fact]
        public void AverageAgeClientTest()
        {
            var clientStorage = new ClientStorage();

            _clientFaker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            clientStorage.AddManyClients(_clientFaker.Generate(1000));

            clientStorage.GetAgeAverage();
        }

        [Fact]
        public void AddEmployeeToStoragePositivTest()
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
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000));

            var employee = _employeeFaker.Generate();

            employeeStorage.AddEmployee(employee);

            Assert.Contains(employee, employeeStorage.GetEmployees());
        }

        [Fact]
        public void AddEmployeeShouldThrowExceptionWhenEmployeeIsNullPositivTest()
        {
            var employeeStorage = new EmployeeStorage();

            Assert.Throws<ArgumentNullException>(() => employeeStorage.AddEmployee(null));
        }

        [Fact]
        public void YoungestEmployeeTest()
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
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000));

            employeeStorage.AddManyEmployees(_employeeFaker.Generate(1000));

            Employee youngestEmployee = employeeStorage.Get(EmployeeMethod.Younger);
        }

        [Fact]
        public void OldestEmployeeTest()
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
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000));

            employeeStorage.AddManyEmployees(_employeeFaker.Generate(1000));

            Employee oldestEmployee = employeeStorage.Get(EmployeeMethod.Older);
        }

        [Fact]
        public void AverageSalaryEmployeeTest()
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
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000));

            employeeStorage.AddManyEmployees(_employeeFaker.Generate(1000));

            employeeStorage.GetSalaryAverage();
        }
    }
}
