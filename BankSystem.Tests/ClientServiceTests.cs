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
                clientService.AddClient(client);
            }
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
            clientService.AddClient(client);

            Assert.Throws<InvalidOperationException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void ClientUnderageThrowPositiveTest()
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

            Assert.Throws<ClientDataException>(() => clientService.AddClient(client));
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

            Assert.Throws<ClientDataException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void ClientAddAdditionalAccountPositiveTest()
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
                    .RuleFor(a => a.Cur, f => new Currency() { Name = "US Dollar", NumCode = "840", Symbol = "$" })
                    .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                    .RuleFor(a => a.AccountNumber, f => $"222484066{f.Random.Number(8).ToString("D7")}");

            var client = _clientFaker.Generate();
            var account = _accountFaker.Generate();

            clientService.AddClient(client);
            clientService.AddAdditionalAccount(client, account);
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

            clientService.AddClient(client);
            clientService.AddAdditionalAccount(client, account);
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

            clientService.AddClient(client);
            clientService.AddAdditionalAccount(client, account);

            Assert.Throws<AccountDataException>(() => clientService.UpdateAccount(client, account, -1000));
        }

        [Fact]
        public void SearchClientNoFilterPositiveTest()
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
                clientService.AddClient(client);
            }

            Assert.Equal(1000, clientStorage.SearchClient().Count());
        }

        [Fact]
        public void GetClientFIOPositiveTest()
        {
            ClientStorage clientStorage = new ClientStorage();
            ClientService clientService = new ClientService(clientStorage);
            string searchFname = string.Empty;
            string searchLname = string.Empty;

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
                clientService.AddClient(client);
                if(i == 500)
                {
                    searchFname = client.FName;
                    searchLname = client.LName;
                }
            }

            Assert.True(clientService.SearchClient(fio: $"{searchFname} {searchLname}").Count > 0);
        }

        [Fact]
        public void GetClientBetweenDatePositiveTest()
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
                clientService.AddClient(client);
            }

            var rand = new Random().Next(0, 1000);

            Assert.True(clientService.SearchClient(dateFrom: new DateOnly(1980, 1, 1), dateTo: new DateOnly(2000, 1, 1)).Count > 0);
        }
    }
}
