using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace BankSystem.App.Services
{
    public class ClientService : IClientStorage
    {
        private readonly IClientStorage _clientStorage;
        private TestDataGenerator _testDataGenerator = new TestDataGenerator();

        public ClientService(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage ?? throw new ArgumentNullException(nameof(clientStorage), "Хранилище клиентов не может быть null.");
        }

        public void Add(Client client)
        {
            ValidateClient(client);
            _clientStorage.Add(client);
        }

        public void Update(Guid id, Client client)
        {
            ValidateClient(client);
            _clientStorage.Update(id, client);
        }

        public void Delete(Guid id)
        {
            _clientStorage.Delete(id);
        }

        public void AddAccount(Guid id)
        {
            _clientStorage.AddAccount(id);
        }

        public void UpdateAccount(Guid id, Account account)
        {
            ValidateAccount(account);
            _clientStorage.UpdateAccount(id, account);
        }

        public void DeleteAccount(Guid id)
        {
            _clientStorage.DeleteAccount(id);
        }

        public List<Client> Get(
            Expression<Func<Client, bool>> filter = null,
            Func<IQueryable<Client>, IOrderedQueryable<Client>> orderBy = null,
            int page = 1,
            int pageSize = 10)
        {
            return _clientStorage.Get(filter, orderBy, page, pageSize);
        }

        public List<Account> GetAccounts(Guid id)
        {
            return _clientStorage.GetAccounts(id);
        }

        private void ValidateClient(Client client)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(client, new ValidationContext(client), validationResults, true))
            {
                throw new ClientDataException($"Неверные данные: {string.Join(", ", validationResults.Select(r => r.ErrorMessage))}");
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - client.BDate.Year;

            if (client.BDate > today.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                throw new ClientDataException("Клиент должен быть старше 18 лет.");
            }

            if (string.IsNullOrEmpty(client.PassportNumber?.Trim()) || string.IsNullOrEmpty(client.PassportSeries?.Trim()))
            {
                throw new ClientDataException("У клиента отсутствуют паспортные данные.");
            }
        }

        private void ValidateAccount(Account account)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(account, new ValidationContext(account), validationResults, true))
            {
                throw new AccountDataException($"Неверные данные лицевого счета: {string.Join(", ", validationResults.Select(r => r.ErrorMessage))}");
            }

            if (account is null)
            {
                throw new ArgumentNullException(nameof(account), "Лицевой счет не может быть null.");
            }

            if (string.IsNullOrEmpty(account.AccountNumber?.Trim()))
            {
                throw new AccountDataException("Номер лицевого счета не может быть пустым.");
            }

            if (account.Amount < 0)
            {
                throw new AccountDataException("Сумма на лицевом счете не может быть отрицательной.");
            }
        }

        public Client GetById(Guid id)
        {
            var client = _clientStorage.GetById(id);

            if (client == null) 
            {
                throw new KeyNotFoundException("Клиент не найден.");
            }

            return client;
        }
    }
}
