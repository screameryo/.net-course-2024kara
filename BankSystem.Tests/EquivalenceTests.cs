using BankSystem.App.Services;
using BankSystem.Domain.Models;
using Xunit;

namespace BankSystem.Tests
{
    public class EquivalenceTests
    {
        [Fact]
        public void GetHashCodeNecessityPositivTest()
        {
            TestDataGenerator generator = new TestDataGenerator();

            Dictionary <Client, Account> clientDictionary = generator.GenerateDictionaryClientsAndAccounts();

            int rand = new Random().Next(0, 100);
            Client newclient = new Client()
            {
                FName = clientDictionary.Keys.ElementAt(rand).FName,
                LName = clientDictionary.Keys.ElementAt(rand).LName,
                MName = clientDictionary.Keys.ElementAt(rand).MName,
                BDate = clientDictionary.Keys.ElementAt(rand).BDate,
                Passport = clientDictionary.Keys.ElementAt(rand).Passport,
                Telephone = clientDictionary.Keys.ElementAt(rand).Telephone,
                Address = clientDictionary.Keys.ElementAt(rand).Address
            };

            Assert.Equal(clientDictionary.Keys.ElementAt(rand).GetHashCode(), newclient.GetHashCode());
        }

        [Fact]
        public void GetHashCodeNecessityPositivManyAccountsTest()
        {
            TestDataGenerator generator = new TestDataGenerator();

            Dictionary <Client, List<Account>> clientDictionary = generator.GenerateDictionaryClientsAndManyAccounts();

            int rand = new Random().Next(0, 100);
            Client newclient = new Client()
            {
                FName = clientDictionary.Keys.ElementAt(rand).FName,
                LName = clientDictionary.Keys.ElementAt(rand).LName,
                MName = clientDictionary.Keys.ElementAt(rand).MName,
                BDate = clientDictionary.Keys.ElementAt(rand).BDate,
                Passport = clientDictionary.Keys.ElementAt(rand).Passport,
                Telephone = clientDictionary.Keys.ElementAt(rand).Telephone,
                Address = clientDictionary.Keys.ElementAt(rand).Address
            };

            Assert.Equal(clientDictionary.Keys.ElementAt(rand).GetHashCode(), newclient.GetHashCode());
        }

        [Fact]
        public void CompareAccountsPositivTest()
        {
            TestDataGenerator generator = new TestDataGenerator();

            Dictionary<Client, Account> clientDictionary = generator.GenerateDictionaryClientsAndAccounts();

            int rand = new Random().Next(0, 100);
            Account newAccount = new Account()
            {
                Currency = clientDictionary.Values.ElementAt(rand).Currency,
                Amount = clientDictionary.Values.ElementAt(rand).Amount
            };

            Assert.Equal(clientDictionary.Values.ElementAt(rand), newAccount);
        }

        [Fact]
        public void CompareAccountsPositivManyAccountsTest()
        {
            TestDataGenerator generator = new TestDataGenerator();

            Dictionary<Client, List<Account>> clientDictionary = generator.GenerateDictionaryClientsAndManyAccounts();

            int rand = new Random().Next(0, 100);

            List<Account> newAccounts = new List<Account>();
            foreach (var account in clientDictionary.Values.ElementAt(rand))
            {
                newAccounts.Add(new Account()
                {
                    Currency = account.Currency,
                    Amount = account.Amount
                });
            }

            Assert.Equal(clientDictionary.Values.ElementAt(rand), newAccounts);
        }

        [Fact]
        public void CompareEmployeePositivTest()
        {
            TestDataGenerator generator = new TestDataGenerator();

            List<Employee> employees = generator.GenerateEmployee();

            int rand = new Random().Next(0, 1000);

            Employee newEmployee = new Employee()
            {
                FName = employees[rand].FName,
                LName = employees[rand].LName,
                MName = employees[rand].MName,
                BDate = employees[rand].BDate,
                Passport = employees[rand].Passport,
                Telephone = employees[rand].Telephone,
                Address = employees[rand].Address,
                Position = employees[rand].Position,
                Department = employees[rand].Department,
                Salary = employees[rand].Salary
            };

            Assert.Equal(employees[rand], newEmployee);
        }
    }
}
