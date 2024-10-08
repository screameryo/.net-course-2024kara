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
                .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(16))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            var client = _clientFaker.Generate();

            clientStorage.Add(client);
        }

        [Fact]
        public void AddEmployeeToStoragePositivTest()
        {
            var employeeStorage = new EmployeeStorage();

            _employeeFaker = new Faker<Employee>()
                .RuleFor(e => e.FName, f => f.Name.FirstName())
                .RuleFor(e => e.LName, f => f.Name.LastName())
                .RuleFor(e => e.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(e => e.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(e => e.PassportNumber, f => f.Random.AlphaNumeric(16))
                .RuleFor(e => e.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                .RuleFor(e => e.Department, f => f.Name.JobArea())
                .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(16));

            var employee = _employeeFaker.Generate();

            employeeStorage.Add(employee);
        }
    }
}
