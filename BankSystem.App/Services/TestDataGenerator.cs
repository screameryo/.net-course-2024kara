using BankSystem.Domain.Models;
using Bogus;

namespace BankSystem.App.Services
{
    public class TestDataGenerator
    {
        /*public List<Employee> GenerateEmployee()
        {
            var faker = new Faker<Employee>()
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
                .RuleFor(e => e.Contract, f => $"Contract to {DateTime.Now.AddMonths(f.Random.Number(1, 24)).ToString("d")}");

            return faker.Generate(1000);
        }

        public List<Client> GenerateClient()
        {
            var faker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            return faker.Generate(1000);
        }

        public Dictionary<string, Client> GenerateClientAsDictionary()
        {
            var faker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            return faker.Generate(1000).ToDictionary(c => c.PassportNumber + c.PassportSeries);
        }

        public Dictionary<Client, Dictionary<string, Account>> GenerateDictionaryClientsAndManyAccounts()
        {
            Dictionary<Client, Dictionary<string, Account>> accountdictionary = new Dictionary<Client, Dictionary<string, Account>>();

            var clientFaker = new Faker<Client>()
                .RuleFor(p => p.FName, f => f.Name.FirstName())
                .RuleFor(p => p.LName, f => f.Name.LastName())
                .RuleFor(p => p.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(p => p.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Address, f => f.Address.FullAddress());
            
            var currencyFaker = new Faker<Currency>()
                .RuleFor(p => p.Name, f => f.Finance.Currency().Description)
                .RuleFor(p => p.NumCode, f => f.Finance.Currency().Code)
                .RuleFor(p => p.Symbol, f => f.Finance.Currency().Symbol);

            var accountFaker = new Faker<Account>()
                .RuleFor(p => p.Cur, f => currencyFaker.Generate())
                .RuleFor(p => p.Amount, f => f.Random.Number(100, 10000))
                .RuleFor(p => p.AccountNumber, f => f.Random.AlphaNumeric(8));

            Dictionary<Client, Dictionary<string, Account>> clientAccountsDictionary = new Dictionary<Client, Dictionary<string, Account>>();

            for (int i = 0; i < 100; i++)
            {
                var account = accountFaker.Generate();
                accountdictionary.Add(clientFaker.Generate(), new Dictionary<string, Account> { { account.AccountNumber, account } }); 
            }

            return accountdictionary;
        }

        public Dictionary<Client, Account> GenerateDictionaryClientsAndAccounts()
        {
            var clientFaker = new Faker<Client>()
                .RuleFor(p => p.FName, f => f.Name.FirstName())
                .RuleFor(p => p.LName, f => f.Name.LastName())
                .RuleFor(p => p.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(p => p.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Address, f => f.Address.FullAddress());

            var currencyFaker = new Faker<Currency>()
                .RuleFor(p => p.Name, f => f.Finance.Currency().Description)
                .RuleFor(p => p.NumCode, f => f.Finance.Currency().Code)
                .RuleFor(p => p.Symbol, f => f.Finance.Currency().Symbol);

            var accountFaker = new Faker<Account>()
                .RuleFor(p => p.Cur, f => currencyFaker.Generate())
                .RuleFor(p => p.Amount, f => f.Random.Number(100, 10000));

            Dictionary<Client, Account> clientAccountsDictionary = new Dictionary<Client, Account>();

            for (int i = 0; i < 100; i++)
            {
                Client client = clientFaker.Generate();
                Account account = accountFaker.Generate();
                clientAccountsDictionary.Add(client, account);
            }

            return clientAccountsDictionary;
        }*/
    }
}
