using BankSystem.App.Services;
using BankSystem.Data;
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
        private readonly BankSystemDbContext _dbContext = new BankSystemDbContext();
        private readonly TestDataGenerator _testDataGenerator = new TestDataGenerator();

        [Fact]
        public void AddClientToStoragePositivTest()
        {
            var clientStorage = new ClientStorage(_dbContext);

            var client = _testDataGenerator.GenerateClient(1).First();

            clientStorage.Add(client);
        }

        [Fact]
        public void AddEmployeeToStoragePositivTest()
        {
            var employeeStorage = new EmployeeStorage(_dbContext);

            var employee = _testDataGenerator.GenerateEmployee(1).First();

            employeeStorage.Add(employee);
        }
    }
}
