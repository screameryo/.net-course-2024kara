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
        private List<Client> _clients = new List<Client>();

        public void AddClient(Client newClient)
        {
            if (newClient == null)
            {
                throw new ArgumentNullException(nameof(newClient), "Клиент не может быть null.");
            }

            _clients.Add(newClient);
        }

        public void AddManyClients(List<Client> newClients)
        {
            if (newClients == null)
            {
                throw new ArgumentNullException(nameof(newClients), "Список клиентов не может быть null.");
            }

            _clients.AddRange(newClients);
        }

        public List<Client> GetClients()
        {
            return _clients;
        }

        public Client Get(ClientMethod method)
        {
            switch (method)
            {
                case ClientMethod.Younger:
                    return _clients.OrderBy(c => c.BDate).First();
                case ClientMethod.Older:
                    return _clients.OrderByDescending(c => c.BDate).First();
                case ClientMethod.Last:
                    return _clients.Last();
                default:
                    throw new InvalidOperationException("Неверный метод.");
            }
        }

        public int GetAgeAverage()
        {
            return (int)_clients.Average(c => DateTime.Now.Year - c.BDate.Year);
        }
    }
}
