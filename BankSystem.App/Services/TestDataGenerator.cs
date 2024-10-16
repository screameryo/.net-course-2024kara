using BankSystem.Domain.Models;
using Bogus;

namespace BankSystem.App.Services
{
    public class TestDataGenerator
    {
        public List<Employee> GenerateEmployee(int count)
        {
            var fakerPosition = new Faker<Position>()
                .RuleFor(p => p.Name, f => f.Name.JobTitle());

            var faker = new Faker<Employee>()
                .RuleFor(e => e.FName, f => f.Name.FirstName())
                .RuleFor(e => e.LName, f => f.Name.LastName())
                .RuleFor(e => e.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                .RuleFor(e => e.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(e => e.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(e => e.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.Salary, f => f.Random.Number(10000, 100000))
                .RuleFor(e => e.Contract, f => $"Contract to {DateTime.Now.AddMonths(f.Random.Number(1, 24)).ToString("d")}");

            return faker.Generate(count);
        }

        public Position GeneratePosition()
        {
            var faker = new Faker<Position>()
                .RuleFor(p => p.Name, f => f.Name.JobTitle());

            return faker.Generate();
        }

        public Department GenerateDepartment()
        {
            var faker = new Faker<Department>()
                .RuleFor(d => d.Name, f => f.Name.JobArea());

            return faker.Generate();
        }

        public List<Client> GenerateClient(int count)
        {
            var faker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-18))))
                .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            return faker.Generate(count);
        }

        public Client GenerateYoungClient()
        {
            var faker = new Faker<Client>()
                .RuleFor(c => c.FName, f => f.Name.FirstName())
                .RuleFor(c => c.LName, f => f.Name.LastName())
                .RuleFor(c => c.BDate, f => DateOnly.FromDateTime(f.Date.Past(18)))
                .RuleFor(c => c.PassportSeries, f => f.Random.AlphaNumeric(2))
                .RuleFor(c => c.PassportNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(c => c.Telephone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            return faker.Generate();
        }


        public List<Position> GeneratePosition(int count) 
        {
            var faker = new Faker<Position>()
                .RuleFor(p => p.Name, f => f.Name.JobTitle());

            return faker.Generate(count);
        }

        public List<Department> GenerateDepartment(int count)
        {
            var faker = new Faker<Department>()
                .RuleFor(d => d.Name, f => f.Name.JobArea());

            return faker.Generate(count);
        }

        public List<Account> GenerateAccount(int count) 
        {
            var faker = new Faker<Account>()
                .RuleFor(a => a.NameCur, f => "USD")
                .RuleFor(a => a.AccountNumber, f => f.Random.AlphaNumeric(10));

            return faker.Generate(count);
        }

    }
}
