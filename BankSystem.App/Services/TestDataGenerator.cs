using BankSystem.Domain.Models;
using Bogus;

namespace BankSystem.App.Services
{
    public class TestDataGenerator
    {
        public List<Employee> GenerateEmployee()
        {
            var faker = new Faker<Employee>()
                .RuleFor(e => e.FName, f => f.Name.FirstName())
                .RuleFor(e => e.LName, f => f.Name.LastName())
                .RuleFor(e => e.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(e => e.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(e => e.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                .RuleFor(e => e.Department, f => f.Name.JobArea())
                .RuleFor(e => e.Contract, f => f.Random.Bool() ? $"Contract to {DateTime.Now.AddMonths(f.Random.Number(1, 24)).ToString("d")}" : null);

            return faker.Generate(1000);
        }

        public List<Client> GenerateClient()
        {
            var faker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.Passport, f => f.Random.AlphaNumeric(8))
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
                .RuleFor(c => c.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            return faker.Generate(1000).ToDictionary(c => c.Passport);
        }

        public Dictionary<Client, List<Account>> GenerateDictionaryClientsAndManyAccounts()
        {
            var clientFaker = new Faker<Client>()
                .RuleFor(p => p.FName, f => f.Name.FirstName())
                .RuleFor(p => p.LName, f => f.Name.LastName())
                .RuleFor(p => p.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(p => p.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(p => p.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Address, f => f.Address.FullAddress());

            var accountFaker = new Faker<Account>()
                .RuleFor(p => p.Currency, f => f.Finance.Currency().Code)
                .RuleFor(p => p.Amount, f => f.Random.Number(100, 10000));

            Dictionary<Client, List<Account>> clientAccountsDictionary = new Dictionary<Client, List<Account>>();

            for (int i = 0; i < 100; i++)
            {
                Client client = clientFaker.Generate();
                List<Account> accountList = accountFaker.Generate(3);
                clientAccountsDictionary.Add(client, accountList);
            }

            return clientAccountsDictionary;
        }

        public Dictionary<Client, Account> GenerateDictionaryClientsAndAccounts()
        {
            var clientFaker = new Faker<Client>()
                .RuleFor(p => p.FName, f => f.Name.FirstName())
                .RuleFor(p => p.LName, f => f.Name.LastName())
                .RuleFor(p => p.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(p => p.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(p => p.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Address, f => f.Address.FullAddress());

            var accountFaker = new Faker<Account>()
                .RuleFor(p => p.Currency, f => f.Finance.Currency().Code)
                .RuleFor(p => p.Amount, f => f.Random.Number(100, 10000));

            Dictionary<Client, Account> clientAccountsDictionary = new Dictionary<Client, Account>();

            for (int i = 0; i < 100; i++)
            {
                Client client = clientFaker.Generate();
                Account account = accountFaker.Generate();
                clientAccountsDictionary.Add(client, account);
            }

            return clientAccountsDictionary;
        }
    }
}
