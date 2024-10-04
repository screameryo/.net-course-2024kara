using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Bogus;
using Xunit;

namespace BankSystem.Tests
{
    public class ClientServiceTests
    {
        private Faker<Client> _clientFaker;
        private Faker<Account> _accountFaker;

        [Fact]
        public void AddClientsPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
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
                clientService.AddClients(client);
            }
        }

        [Fact]
        public void AccCLientAndCheckContainsUSDAccountPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
            ClientService clientService = new ClientService(clientStorage);

            _clientFaker = new Faker<Client>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress());

            _accountFaker = new Faker<Account>()
                    .RuleFor(a => a.Cur, f => new Currency())
                    .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                    .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            var client = _clientFaker.Generate();
            var account = _accountFaker.Generate();

            clientService.AddClients(client);
            clientService.AddAccountToExistingClient(client, account);

            Assert.Contains(clientStorage.GetClients()[client].Values, account => account.Cur.NumCode == "840");
        }

        [Fact]
        public void ClientExistingThrowPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
            ClientService clientService = new ClientService(clientStorage);

            _clientFaker = new Faker<Client>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress());

            var client = _clientFaker.Generate();
            clientService.AddClients(client);

            Assert.Throws<ClientDataExceptions>(() => clientService.AddClients(client));
        }

        [Fact]
        public void ClientUnderageClientThrowPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
            ClientService clientService = new ClientService(clientStorage);

            _clientFaker = new Faker<Client>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(18)))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress());

            var client = _clientFaker.Generate();

            Assert.Throws<ClientDataExceptions>(() => clientService.AddClients(client));
        }

        [Fact]
        public void ClientNoPassportDataNoSeriesThrowPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
            ClientService clientService = new ClientService(clientStorage);

            _clientFaker = new Faker<Client>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress());

            var client = _clientFaker.Generate();

            Assert.Throws<ClientDataExceptions>(() => clientService.AddClients(client));
        }

        [Fact]
        public void ClientAddAccountPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
            ClientService clientService = new ClientService(clientStorage);

            _clientFaker = new Faker<Client>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress());

            _accountFaker = new Faker<Account>()
                    .RuleFor(a => a.Cur, f => new Currency())
                    .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                    .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            var client = _clientFaker.Generate();
            var account = _accountFaker.Generate();

            clientService.AddClients(client);
            clientService.AddAccountToExistingClient(client, account);
        }

        [Fact]
        public void ClientChangeAmountAccountPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
            ClientService clientService = new ClientService(clientStorage);

            _clientFaker = new Faker<Client>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress());

            _accountFaker = new Faker<Account>()
                    .RuleFor(a => a.Cur, f => new Currency())
                    .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                    .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            var client = _clientFaker.Generate();
            var account = _accountFaker.Generate();

            clientService.AddClients(client);
            clientService.AddAccountToExistingClient(client, account);
            clientService.UpdateAccount(client, account, 1000);
        }

        [Fact]
        public void ClientChangeAmountAccountLessThanZeroThrowPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
            ClientService clientService = new ClientService(clientStorage);

            _clientFaker = new Faker<Client>()
                    .RuleFor(c => c.FName, f => f.Name.FirstName())
                    .RuleFor(c => c.LName, f => f.Name.LastName())
                    .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                    .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                    .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                    .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress());

            _accountFaker = new Faker<Account>()
                    .RuleFor(a => a.Cur, f => new Currency())
                    .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                    .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            var client = _clientFaker.Generate();
            var account = _accountFaker.Generate();

            clientService.AddClients(client);
            clientService.AddAccountToExistingClient(client, account);

            Assert.Throws<AccountDataExceptions>(() => clientService.UpdateAccount(client, account, -1000));
        }

        [Fact]
        public void GetClientsNoFilterPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
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
                clientService.AddClients(client);
            }

            Assert.Equal(1000, clientService.GetClients().Count);
        }

        [Fact]
        public void GetClientsFIOPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
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
                clientService.AddClients(client);
            }

            var rand = new System.Random().Next(0, 1000);

            Assert.True(clientService.GetClients($"{clientStorage.GetClients().Keys.ElementAt(rand).FName} {clientStorage.GetClients().Keys.ElementAt(rand).LName}").Count > 0);
        }

        [Fact]
        public void GetClientsBetweenDatePositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
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
                clientService.AddClients(client);
            }

            var rand = new System.Random().Next(0, 1000);

            Assert.True(clientService.GetClients(dateFrom: new DateOnly(1980, 1, 1), dateTo: new DateOnly(2000, 1, 1)).Count > 0);
        }
    }
}
