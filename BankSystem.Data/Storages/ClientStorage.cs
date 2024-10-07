using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public enum ClientMethod
    {
        Younger,
        Older
    }

    public class ClientStorage
    {
        private Dictionary<Client, List<Account>> _clients = new Dictionary<Client, List<Account>>();

        public void AddClient(Client newClient, List<Account> newAccount)
        {
            if(!_clients.TryAdd(newClient, newAccount))
            {
                throw new InvalidOperationException("Клиент уже существует.");
            }
        }

        public Client Get(ClientMethod method)
        {
            if (_clients is null)
            {
                throw new ArgumentNullException(nameof(_clients), "Список клиентов не может быть null.");
            }

            switch (method)
            {
                case ClientMethod.Younger:
                    return _clients.Keys.MinBy(c => c.BDate);
                case ClientMethod.Older:
                    return _clients.Keys.MaxBy(c => c.BDate);
                default:
                    throw new InvalidOperationException("Неверный метод.");
            }
        }

        public int GetAgeAverage()
        {
            if(_clients is null)
            {
                throw new ArgumentNullException(nameof(_clients), "Список клиентов не может быть null.");
            }

            return (int)_clients.Keys.Average(c => DateTime.Now.Year - c.BDate.Year);
        }

        public void AddAccountToClient(Client client, Account account)
        {
            if(_clients[client].Contains(account))
            {
                throw new InvalidOperationException("Лицевой счет уже существует.");
            }
            else
            {
                _clients[client].Add(account);
            }
        }

        public void RemoveAccountFromClient(Client client, Account account)
        {
            _clients[client].Remove(account);
        }

        public void UpdateAccount(Client client, Account account, int newAmount)
        {
            _clients[client].Where(a => a == account).First().Amount = newAmount;
        }

        public bool ContainsClient(Client client)
        {
           return _clients.ContainsKey(client) ? true : false;
        }

        public Dictionary<Client, List<Account>> SearchClient(string fio = "", string phone = "", string passport = "", DateOnly? dateFrom = null, DateOnly? dateTo = null)
        {
            if (string.IsNullOrEmpty(fio) && string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(passport) && dateFrom == null && dateTo == null)
            {
                return _clients;
            }

            Expression<Func<Client, bool>> filter = c =>
                (string.IsNullOrEmpty(fio) || c.GetFullName().StartsWith(fio) || c.GetFullName().EndsWith(fio) || c.GetFullName().IndexOf(fio) >= 0) &&
                (string.IsNullOrEmpty(phone) || c.Telephone.StartsWith(phone) || c.Telephone.EndsWith(phone) || c.Telephone.IndexOf(phone) >= 0) &&
                (string.IsNullOrEmpty(passport) || c.PassportNumber.StartsWith(passport) || c.PassportNumber.EndsWith(passport) || c.PassportNumber.IndexOf(passport) >= 0) &&
                (!dateFrom.HasValue || c.BDate >= dateFrom) &&
                (!dateTo.HasValue || c.BDate <= dateTo);

            return _clients.Where(c => filter.Compile()(c.Key)).ToDictionary(c => c.Key, c => c.Value);
        }
    }
}
