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
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.Ch_Account, f => f.Random.Number(100, 10000).ToString());

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
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.Ch_Account, f => f.Random.Number(100, 10000).ToString());

            return faker.Generate(1000).ToDictionary(c => c.Passport);
        }
    }
}
