using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage
    {
        public List<Client> clients = new List<Client>();

        public void AddClient(Client newClient)
        {
            if (newClient == null)
            {
                throw new ArgumentNullException(nameof(newClient), "Клиент не может быть null.");
            }

            clients.Add(newClient);
        }

        public void AddManyClients(List<Client> newClients)
        {
            if (newClients == null)
            {
                throw new ArgumentNullException(nameof(newClients), "Список клиентов не может быть null.");
            }

            clients.AddRange(newClients);
        }

        public Client YoungestClient(List<Client> clients)
        {
            if (clients == null)
            {
                throw new ArgumentNullException(nameof(clients), "Список клиентов не может быть null.");
            }

            if (clients.Count == 0)
            {
                throw new ArgumentException("Список клиентов не может быть пустым.", nameof(clients));
            }

            return clients.OrderBy(c => c.BDate).First();
        }

        public Client OldestClient(List<Client> clients)
        {
            if (clients == null)
            {
                throw new ArgumentNullException(nameof(clients), "Список клиентов не может быть null.");
            }

            if (clients.Count == 0)
            {
                throw new ArgumentException("Список клиентов не может быть пустым.", nameof(clients));
            }

            return clients.OrderByDescending(c => c.BDate).First();
        }

        public int AverageAge()
        {
            if (clients == null)
            {
                throw new ArgumentNullException(nameof(clients), "Список клиентов не может быть null.");
            }

            if (clients.Count == 0)
            {
                throw new ArgumentException("Список клиентов не может быть пустым.", nameof(clients));
            }

            return (int)clients.Average(c => DateTime.Now.Year - c.BDate.Year);
        }
    }
}
