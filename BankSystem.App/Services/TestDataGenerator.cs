using BankSystem.Domain.Models;
using Bogus;

namespace BankSystem.App.Services
{
    public class TestDataGenerator
    {
        public void GenerateEmployee(ref List<Employee> employees)
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

            for (int i = 0; i < 1000; i++)
            {
                employees.Add(faker.Generate());
            }
        }

        public void GenerateClient(ref List<Client> clients)
        {
            var faker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.Ch_Account, f => f.Random.Number(100, 10000).ToString());

            for (int i = 0; i < 1000; i++)
            {
                clients.Add(faker.Generate());
            }
        }

        public void GenerateClientAsDictionary(ref Dictionary<string, Client> clients)
        {
            var faker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50)))
                .RuleFor(c => c.Passport, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.Ch_Account, f => f.Random.Number(100, 10000).ToString());

            for (int i = 0; i < 1000; i++)
            {
                var client = faker.Generate();
                try
                {
                    clients.Add(client.Telephone, client);
                }
                catch (Exception)
                {
                    i--;
                }
                
            }
        }
    }
}
