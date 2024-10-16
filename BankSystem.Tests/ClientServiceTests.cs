using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.Data;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BankSystem.Tests
{
    public class ClientServiceTests
    {
        private Faker<Client> _clientFaker;
        private Faker<Account> _accountFaker;
        private BankSystemDbContext _dbContext = new BankSystemDbContext();
        private TestDataGenerator _testDataGenerator = new TestDataGenerator();

        [Fact]
        public void AddClientsPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);            

            for (int i = 0; i < 100; i++)
            {
                var client = _testDataGenerator.GenerateClient(1).First();
                clientService.Add(client);
            }
        }

        [Fact]
        public void ClientGetByIdPosiiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);

            var employee = _testDataGenerator.GenerateClient(1).First();

            clientService.Add(employee);

            Assert.Equal(employee, clientService.Get(f => f.Id == employee.Id).First());
        }

        [Fact]
        public void ClientUpdatePositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);

            var client = _testDataGenerator.GenerateClient(1).First();
            var newClient = _testDataGenerator.GenerateClient(1).First();

            clientService.Add(client);
            clientService.Update(client.Id, newClient);
        }

        [Fact]
        public void ClientExistingThrowPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);

            var client = _testDataGenerator.GenerateClient(1).First();
            clientService.Add(client);

            Assert.Throws<DbUpdateException>(() => clientService.Add(client));
        }

        [Fact]
        public void ClientUnderageThrowPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);

            var client = _testDataGenerator.GenerateYoungClient();

            Assert.Throws<ClientDataException>(() => clientService.Add(client));
        }

        [Fact]
        public void ClientNoPassportDataNoSeriesThrowPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);

            _clientFaker = new Faker<Client>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress());

            var client = _clientFaker.Generate();

            Assert.Throws<ClientDataException>(() => clientService.Add(client));
        }

        [Fact]
        public void ClientAddAdditionalAccountPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);

            var client = _testDataGenerator.GenerateClient(1).First();

            clientService.Add(client);
            clientService.AddAccount(client.Id);
        }

        [Fact]
        public void ClientChangeAmountAccountPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);

            var client = _testDataGenerator.GenerateClient(1).First();
            var account = _testDataGenerator.GenerateAccount(1).First();

            clientService.Add(client);
            clientService.AddAccount(client.Id);            

            clientService.UpdateAccount(clientService.GetAccounts(client.Id).First().Id, account);
        }

        [Fact]
        public void SearchClientNoFilterPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);

            for (int i = 0; i < 100; i++)
            {
                var client = _testDataGenerator.GenerateClient(1).First();
                clientService.Add(client);
            }

            Assert.Equal(10, clientStorage.Get().Count());
        }

        [Fact]
        public void GetClientFIOPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);
            string searchFname = string.Empty;
            string searchLname = string.Empty;

            for (int i = 0; i < 100; i++)
            {
                var client = _testDataGenerator.GenerateClient(1).First();
                clientService.Add(client);
                if(i == 20)
                {
                    searchFname = client.FName;
                    searchLname = client.LName;
                }
            }

            Assert.True(clientService.Get(f => f.FName.Equals(searchFname)
                                            && f.LName.Equals(searchLname)).Count > 0);
        }

        [Fact]
        public void GetClientBetweenDatePositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);

            _clientFaker = new Faker<Client>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress());

            for (int i = 0; i < 1000; i++)
            {
                var client = _clientFaker.Generate();
                clientService.Add(client);
            }
            Assert.True(clientService.Get(f => f.BDate >= new DateOnly(2000, 1, 1)
                                            && f.BDate <= new DateOnly(2024, 1, 1)).Count > 0);
        }

        [Fact]
        public void ClientDeletePositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);

            var client = _testDataGenerator.GenerateClient(1).First();

            clientService.Add(client);
            clientService.Delete(client.Id);
        }

        [Fact]
        public void AccountDeletePositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage(_dbContext);
            ClientService clientService = new ClientService(clientStorage);

            var client = _testDataGenerator.GenerateClient(1).First();

            clientService.Add(client);
            clientService.AddAccount(client.Id);

            List<Account> accounts = clientService.GetAccounts(client.Id);

            clientService.DeleteAccount(accounts.First().Id);   
        }
    }
}
