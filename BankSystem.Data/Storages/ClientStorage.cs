using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class ClientStorage : IClientStorage
    {
        private Dictionary<Client, List<Account>> _clients = new Dictionary<Client, List<Account>>();

        public Dictionary<Client, List<Account>> Data => _clients;

        public void Add(Client newClient)
        {
            if (!_clients.TryAdd(newClient, new List<Account>()))
            {
                throw new InvalidOperationException("Клиент уже существует.");
            }
        }

        public void Update(Client client)
        {
            if (!_clients.ContainsKey(client))
            {
                throw new InvalidOperationException("Клиент не найден.");
            }
        }

        public void Delete(Client client)
        {
            if (!_clients.Remove(client))
            {
                throw new InvalidOperationException("Клиент не найден.");
            }
        }

        public void AddAccount(Client client, Account account)
        {
            if (_clients[client].Contains(account))
            {
                throw new InvalidOperationException("Лицевой счет уже существует.");
            }
            else
            {
                _clients[client].Add(account);
            }
        }

        public void UpdateAccount(Client client, Account account)
        {
            if (!_clients[client].Contains(account))
            {
                throw new InvalidOperationException("Лицевой счет не найден.");
            }
        }

        public void DeleteAccount(Client client, Account account)
        {
            if (!_clients[client].Remove(account))
            {
                throw new InvalidOperationException("Лицевой счет не найден.");
            }
        }

        public List<Client> Get(
            Expression<Func<Client, bool>>? filter = null,
            Func<IQueryable<Client>, IOrderedQueryable<Client>>? orderBy = null,
            int page = 1,
            int pageSize = 10)
        {
            var query = _clients.Keys.AsQueryable();

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
    }
}
