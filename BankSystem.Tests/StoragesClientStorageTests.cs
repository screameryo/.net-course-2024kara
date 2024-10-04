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
        private Faker<Account> _accountFaker;

        [Fact]
        public void AddClientToStoragePositivTest()
        {
            var clientStorage = new ClientStorage();

            _clientFaker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            _accountFaker = new Faker<Account>()
                .RuleFor(a => a.Cur, f => new Currency { Name = f.Finance.Currency().Description, NumCode = f.Finance.Currency().Code, Symbol = f.Finance.Currency().Symbol })
                .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            var client = _clientFaker.Generate();
            var account = _accountFaker.Generate();

            clientStorage.AddClient(client, new Dictionary<string, Account> { { account.AccountNumber, account } });

            Assert.Contains(client, clientStorage.GetClients());
        }

        [Fact]
        public void YoungestClientTest()
        {
            var clientStorage = new ClientStorage();

            _clientFaker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            _accountFaker = new Faker<Account>()
                .RuleFor(a => a.Cur, f => new Currency { Name = f.Finance.Currency().Description, NumCode = f.Finance.Currency().Code, Symbol = f.Finance.Currency().Symbol })
                .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            for (int i = 0; i < 1000; i++)
            {
                var account = _accountFaker.Generate();
                clientStorage.AddClient(_clientFaker.Generate(), new Dictionary<string, Account> { { account.AccountNumber, account } });
            }

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
                .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            _accountFaker = new Faker<Account>()
                .RuleFor(a => a.Cur, f => new Currency { Name = f.Finance.Currency().Description, NumCode = f.Finance.Currency().Code, Symbol = f.Finance.Currency().Symbol })
                .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            for (int i = 0; i < 1000; i++)
            {
                var account = _accountFaker.Generate();
                clientStorage.AddClient(_clientFaker.Generate(), new Dictionary<string, Account> { { account.AccountNumber, account } });
            }

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
                .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            _accountFaker = new Faker<Account>()
                .RuleFor(a => a.Cur, f => new Currency { Name = f.Finance.Currency().Description, NumCode = f.Finance.Currency().Code, Symbol = f.Finance.Currency().Symbol })
                .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            for (int i = 0; i < 1000; i++)
            {
                var account = _accountFaker.Generate();
                clientStorage.AddClient(_clientFaker.Generate(), new Dictionary<string, Account> { { account.AccountNumber, account } });
            }

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
                .RuleFor(e => e.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(e => e.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(e => e.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                .RuleFor(e => e.Department, f => f.Name.JobArea())
                .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            _accountFaker = new Faker<Account>()
                .RuleFor(a => a.Cur, f => new Currency { Name = f.Finance.Currency().Description, NumCode = f.Finance.Currency().Code, Symbol = f.Finance.Currency().Symbol })
                .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            var employee = _employeeFaker.Generate();
            var account = _accountFaker.Generate();

            employeeStorage.AddEmployee(employee, new Dictionary<string, Account> { { account.AccountNumber, account } });

            Assert.Contains(employee, employeeStorage.GetEmployees());
        }

        [Fact]
        public void YoungestEmployeeTest()
        {
            var employeeStorage = new EmployeeStorage();

            _employeeFaker = new Faker<Employee>()
                .RuleFor(e => e.FName, f => f.Name.FirstName())
                .RuleFor(e => e.LName, f => f.Name.LastName())
                .RuleFor(e => e.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(e => e.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(e => e.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(e => e.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                .RuleFor(e => e.Department, f => f.Name.JobArea())
                .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            _accountFaker = new Faker<Account>()
                .RuleFor(a => a.Cur, f => new Currency { Name = f.Finance.Currency().Description, NumCode = f.Finance.Currency().Code, Symbol = f.Finance.Currency().Symbol })
                .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            for (int i = 0; i < 1000; i++)
            {
                var account = _accountFaker.Generate();
                employeeStorage.AddEmployee(_employeeFaker.Generate(), new Dictionary<string, Account> { { account.AccountNumber, account } });
            }

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
                .RuleFor(e => e.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(e => e.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(e => e.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                .RuleFor(e => e.Department, f => f.Name.JobArea())
                .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            _accountFaker = new Faker<Account>()
                .RuleFor(a => a.Cur, f => new Currency { Name = f.Finance.Currency().Description, NumCode = f.Finance.Currency().Code, Symbol = f.Finance.Currency().Symbol })
                .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            for (int i = 0; i < 1000; i++)
            {
                var account = _accountFaker.Generate();
                employeeStorage.AddEmployee(_employeeFaker.Generate(), new Dictionary<string, Account> { { account.AccountNumber, account } });
            }

            Employee oldestEmployee = employeeStorage.Get(EmployeeMethod.Older);
        }

        [Fact]
        public void AverageAgeEmployeeTest()
        {
            var employeeStorage = new EmployeeStorage();

            _employeeFaker = new Faker<Employee>()
                .RuleFor(e => e.FName, f => f.Name.FirstName())
                .RuleFor(e => e.LName, f => f.Name.LastName())
                .RuleFor(e => e.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(e => e.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(e => e.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(e => e.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                .RuleFor(e => e.Department, f => f.Name.JobArea())
                .RuleFor(e => e.Contract, f => f.Random.AlphaNumeric(8));

            _accountFaker = new Faker<Account>()
                .RuleFor(a => a.Cur, f => new Currency { Name = f.Finance.Currency().Description, NumCode = f.Finance.Currency().Code, Symbol = f.Finance.Currency().Symbol })
                .RuleFor(a => a.Amount, f => f.Random.Number(100, 10000))
                .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(8));

            for (int i = 0; i < 1000; i++)
            {
                var account = _accountFaker.Generate();
                employeeStorage.AddEmployee(_employeeFaker.Generate(), new Dictionary<string, Account> { { account.AccountNumber, account } });
            }

            employeeStorage.GetAgeAverage();
        }
    }
}
