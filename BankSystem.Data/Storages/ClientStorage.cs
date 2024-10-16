using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.App.Services;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class ClientStorage : IClientStorage
    {
        private readonly BankSystemDbContext _dbContext;
        private readonly TestDataGenerator _testDataGenerator = new TestDataGenerator();

        public ClientStorage(BankSystemDbContext bankSystemDbContext)
        {
            _dbContext = bankSystemDbContext;
        }

        public void Add(Client newClient)
        {
            try
            {
                _dbContext.Clients.Add(newClient);

                var account = new Account()
                {
                    NameCur = "USD",
                    Amount = 0,
                    ClientId = newClient.Id,
                    AccountNumber = "1234567890"
                };

                _dbContext.Accounts.Add(account);

                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ошибка при добавлении клиента.", ex);
            }
        }

        public void Update(Guid id, Client client)
        {
            var clientToUpdate = _dbContext.Clients.Find(id);

            if (clientToUpdate == null)
            {
                throw new InvalidOperationException("Клиент не найден.");
            }

            clientToUpdate.FName = client.FName;
            clientToUpdate.LName = client.FName;
            clientToUpdate.MName = client.MName;
            clientToUpdate.Telephone = client.Telephone;
            clientToUpdate.Address = client.Address;
            clientToUpdate.PassportNumber = client.PassportNumber;
            clientToUpdate.PassportSeries = client.PassportSeries;
            clientToUpdate.BDate = client.BDate;

            try
            {
                _dbContext.Clients.Update(clientToUpdate);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ошибка при обновлении клиента.", ex);
            }
        }

        public void Delete(Guid id)
        {
            var clientToDelete = _dbContext.Clients.Find(id);

            if (clientToDelete == null)
            {
                throw new KeyNotFoundException("Клиент не найден.");
            }

            try
            {
                _dbContext.Clients.Remove(clientToDelete);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ошибка при удалении клиента.", ex);
            }
        }

        public void AddAccount(Guid id)
        {
            Account account = _testDataGenerator.GenerateAccount(1)[0];

            var clientToUpdate = _dbContext.Clients.Find(id);

            if (clientToUpdate == null)
            {
                throw new KeyNotFoundException("Клиент не найден.");
            }

            account.ClientId = id;
            clientToUpdate.Accounts.Add(account);

            try
            {
                _dbContext.Clients.Update(clientToUpdate);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ошибка при добавлении лицевого счета.", ex);
            }

        }

        public void UpdateAccount(Guid id, Account account)
        {
            var accountToUpdate = _dbContext.Accounts.Find(id);

            if (accountToUpdate == null)
            {
                throw new KeyNotFoundException("Лицевой счет не найден.");
            }

            accountToUpdate.Amount = account.Amount;
            accountToUpdate.NameCur = account.NameCur;
            accountToUpdate.AccountNumber = account.AccountNumber;

            try
            {
                _dbContext.Accounts.Update(accountToUpdate);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ошибка при обновлении лицевого счета.", ex);
            }
        }

        public void DeleteAccount(Guid id)
        {
            var accountToDelete = _dbContext.Accounts.Find(id);

            if (accountToDelete == null)
            {
                throw new KeyNotFoundException("Лицевой счет не найден.");
            }

            try
            {
                _dbContext.Accounts.Remove(accountToDelete);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Ошибка при удалении лицевого счета.", ex);
            }
        }

        public List<Client> Get(
            Expression<Func<Client, bool>>? filter = null,
            Func<IQueryable<Client>, IOrderedQueryable<Client>>? orderBy = null,
            int page = 1,
            int pageSize = 10)
        {
            var query = _dbContext.Clients.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<Account> GetAccounts(Guid id)
        {
            var accounts = _dbContext.Accounts
                .Where(c => c.ClientId == id);

            if (accounts == null)
            {
                throw new ClientDataException("Лицевые счета не найдены.");
            }

            return accounts.ToList();
        }

        public Client? GetClientById(Guid id)
        {
            return _dbContext.Clients.Find(id);
        }

        public Client GetById(Guid id)
        {
            return _dbContext.Clients.Find(id);
        }
    }
}
