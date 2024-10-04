using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public enum ClientMethod
    {
        Younger,
        Older,
        Last
    }

    public class ClientStorage
    {
        private Dictionary<Client, Dictionary<string, Account>> _clients = new Dictionary<Client, Dictionary<string, Account>>();

        public void AddClient(Client newClient, Dictionary<string, Account> newAccount)
        {
            _clients.Add(newClient, newAccount);
        }

        public Dictionary<Client, Dictionary<string, Account>> GetClients()
        {
            return _clients;
        }

        public Client Get(ClientMethod method)
        {
            switch (method)
            {
                case ClientMethod.Younger:
                    return _clients.Keys.OrderBy(c => c.BDate).First();
                case ClientMethod.Older:
                    return _clients.Keys.OrderByDescending(c => c.BDate).First();
                case ClientMethod.Last:
                    return _clients.Keys.Last();
                default:
                    throw new InvalidOperationException("Неверный метод.");
            }
        }

        public int GetAgeAverage()
        {
            return (int)_clients.Keys.Average(c => DateTime.Now.Year - c.BDate.Year);
        }

        public void AddAccountToClient(Client client, Account account)
        {
            _clients[client].Add(account.AccountNumber, account);
        }

        public void RemoveAccountFromClient(Client client, Account account)
        {
            _clients[client].Remove(account.AccountNumber);
        }

        public void UpdateAccount(Client client, Account account, int newAmount)
        {
            account.Amount = (int)newAmount;
        }
    }
}
