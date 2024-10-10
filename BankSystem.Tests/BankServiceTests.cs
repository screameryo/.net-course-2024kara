using BankSystem.App.Services;
using BankSystem.Domain.Models;
using Xunit;
using Person = BankSystem.Domain.Models.Person;

namespace BankSystem.Tests
{
    public class BankServiceTests
    {
        [Fact]
        public void AddBonusToEmployeeTest()
        {
            var bankService = new BankService();
            var employee = new Employee
            {
                FName = "John",
                LName = "Doe",
                BDate = new DateOnly(1985, 5, 20),
                PassportNumber = "123456",
                PassportSeries = "AB",
                Address = "123 Main St",
                Telephone = "+1234567890",
                Position = "Developer",
                Department = "IT",
                Salary = 50000,
                Bonuses = new List<string>()
            };

            bankService.AddBonus(employee, "NewYear bonus");

            Assert.Contains("NewYear bonus", employee.Bonuses);
        }

        [Fact]
        public void AddBonusToClientTest()
        {
            var bankService = new BankService();
            var client = new Client
            {
                FName = "Jane",
                LName = "Smith",
                BDate = new DateOnly(1990, 3, 15),
                PassportNumber = "654321",
                PassportSeries = "CD",
                Address = "456 Elm St",
                Telephone = "+0987654321",
                Bonuses = new List<string>()
            };

            bankService.AddBonus(client, "Loyalty bonus");

            Assert.Contains("Loyalty bonus", client.Bonuses);
        }

        [Fact]
        public void AddBonusToPersonTest()
        {
            var bankService = new BankService();
            var person = new Person
            {
                FName = "Alice",
                LName = "Johnson",
                BDate = new DateOnly(1975, 7, 10),
                PassportNumber = "789012",
                PassportSeries = "EF",
                Address = "789 Pine St",
                Telephone = "+1122334455",
                Bonuses = new List<string>()
            };

            bankService.AddBonus(person, "Special bonus");

            Assert.Contains("Special bonus", person.Bonuses);
        }

        [Fact]
        public void AddClientToBlackListTest()
        {
            var bankService = new BankService();
            var client = new Client
            {
                FName = "Jane",
                LName = "Smith",
                BDate = new DateOnly(1990, 3, 15),
                PassportNumber = "654321",
                PassportSeries = "CD",
                Address = "456 Elm St",
                Telephone = "+0987654321",
                Bonuses = new List<string>()
            };

            bankService.AddToBlackList(client);

            Assert.True(bankService.IsPersonInBlackList(client));
        }

        [Fact]
        public void AddEmployeeToBlackListTest()
        {
            var bankService = new BankService();
            var employee = new Employee
            {
                FName = "John",
                LName = "Doe",
                BDate = new DateOnly(1985, 5, 20),
                PassportNumber = "123456",
                PassportSeries = "AB",
                Address = "123 Main St",
                Telephone = "+1234567890",
                Position = "Developer",
                Department = "IT",
                Salary = 50000,
                Bonuses = new List<string>()
            };

            bankService.AddToBlackList(employee);

            Assert.True(bankService.IsPersonInBlackList(employee));
        }
    }
}
