using BankSystem.App.Exceptions;
using BankSystem.App.Filter;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Numerics;

namespace BankSystem.App.Services
{
    public class ClientService : IClientStorage
    {
        private readonly IClientStorage _clientStorage;

        public Dictionary<Client, List<Account>> Data => _clientStorage.Data;

        public ClientService(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage ?? throw new ArgumentNullException(nameof(clientStorage), "Хранилище клиентов не может быть null.");
        }

        public void Add(Client item)
        {
            ValidateClient(item);
            _clientStorage.Add(item);
        }

        public void Update(Client item)
        {
            ValidateClient(item);
            _clientStorage.Update(item);
        }

        public void Delete(Client item)
        {
            _clientStorage.Delete(item);
        }

        public void AddAccount(Client client, Account account)
        {
            ValidateClient(client);
            ValidateAccount(account);
            _clientStorage.AddAccount(client, account);
        }

        public void UpdateAccount(Client client, Account account)
        {
            ValidateClient(client);
            ValidateAccount(account);
            _clientStorage.UpdateAccount(client, account);
        }

        public void DeleteAccount(Client client, Account account)
        {
            _clientStorage.DeleteAccount(client, account);
        }

        public List<Client> Get(
            Client item,
            Expression<Func<Client, bool>> filter = null,
            Func<IQueryable<Client>, IOrderedQueryable<Client>> orderBy = null,
            int page = 1,
            int pageSize = 10)
        {
            return _clientStorage.Get(item, filter, orderBy, page, pageSize);
        }



        private void ValidateClient(Client client)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(client, new ValidationContext(client), validationResults, true))
            {
                throw new ClientDataException($"Неверные данные лицевого счета: {string.Join(", ", validationResults.Select(r => r.ErrorMessage))}");
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
    }
}
