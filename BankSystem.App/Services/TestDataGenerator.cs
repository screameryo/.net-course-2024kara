using BankSystem.Domain.Models;
using Bogus;

namespace BankSystem.App.Services
{
    public class TestDataGenerator
    {
        /// <summary>
        /// Generate a list of employees
        /// </summary>
        /// <param name="countemployees"></param>
        /// <param name="employees"></param>
        public void GenerateEmployee(int countemployees, ref List<Employee> employees)
        {
            //using bogus generate 10000 fake employee data
            var faker = new Faker<Employee>()
                .CustomInstantiator(f => new Employee(
                    f.Person.FirstName,
                    f.Person.LastName,
                    DateOnly.FromDateTime(f.Date.Past(50)),
                    f.Random.AlphaNumeric(8),
                    f.Address.FullAddress(),
                    f.Name.JobTitle(),
                    f.Random.Number(1000, 10000),
                    f.Random.AlphaNumeric(8),
                    f.Phone.PhoneNumber(),
                    f.Random.Bool() ? $"Contract to {f.Date.Future(2).ToString("d")}" : null,                    
                    f.Random.Bool() ? f.Name.Suffix() : null
                ));

            for (int i = 0; i < countemployees; i++)
            {
                employees.Add(faker.Generate());
            }
        }

        /// <summary>
        /// Generate a list of clients
        /// </summary>
        /// <param name="countclients"></param>
        /// <param name="clients"></param>
        public void GenerateClient(int countclients, ref List<Client> clients)
        {
            //using bogus generate 10000 fake client data
            var faker = new Faker<Client>()
                .CustomInstantiator(f => new Client(
                    f.Person.FirstName,
                    f.Person.LastName,
                    DateOnly.FromDateTime(f.Date.Past(50)),
                    f.Random.AlphaNumeric(8),
                    f.Address.FullAddress(),
                    f.Phone.PhoneNumber(),
                    f.Random.Bool() ? f.Name.Suffix() : null
                ));

            for (int i = 0; i < countclients; i++)
            {
                clients.Add(faker.Generate());
            }
        }
    }
}
