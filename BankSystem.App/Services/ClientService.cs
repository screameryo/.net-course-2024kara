using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private readonly ClientStorage _clientStorage;
        private readonly string KUBBank = "66";

        public ClientService(ClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public void AddClients(Client newClient)
        {
            if (_clientStorage.GetClients().ContainsKey(newClient))
            {
                throw new ClientDataExceptions(ClientDataExceptions.ClientAlreadyExistsMessage);
            }

            Account newAccount = CreateAccountForNewClient();

            if (ValidateClient(newClient))
            {
                _clientStorage.AddClient(newClient, new Dictionary<string, Account> { { newAccount.AccountNumber, newAccount } });
            }
        }

        public void AddAccountToExistingClient(Client client, Account account)
        {
            if (ValidateClient(client))
            {
                AddAccountToClient(client, account);
            }
        }

        public void AddAccountToClient(Client client, Account account)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "Клиент не может быть null.");
            }

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Лицевой счет не может быть null.");
            }

            if (_clientStorage.GetClients().ContainsKey(client))
            {
                _clientStorage.AddAccountToClient(client, account);
            }
            else
            {
                throw new ClientDataExceptions(ClientDataExceptions.NoClientMessage);
            }
        }

        private bool ValidateClient(Client client)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - client.BDate.Year;

            if (client.BDate > today.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                throw new ClientDataExceptions(ClientDataExceptions.UnderageClientMessage);
            }

            if(client.PassportNumber is null || client.PassportSeries is null)
            {
                throw new ClientDataExceptions(ClientDataExceptions.NoPassportDataMessage);
            }

            if (string.IsNullOrEmpty(client.PassportNumber.Trim()) || string.IsNullOrEmpty(client.PassportSeries.Trim()))
            {
                throw new ClientDataExceptions(ClientDataExceptions.NoPassportDataMessage);
            }

            return true;
        }

        private Account CreateAccountForNewClient()
        {
            Currency currency = new Currency { Name = "US Dollar", NumCode = "840", Symbol = "$" };
            var account = new Account
            {
                Cur = currency,
                Amount = 0,
                AccountNumber = $"2224{currency.NumCode}{KUBBank}{GenerateUniqueRandomAcc(currency.NumCode)}"
            };

            return account;
        }

        private string GenerateUniqueRandomAcc(string numcode)
        { 
            Random random = new Random();
            int randomNumber = random.Next(0, 10000000);
            var clients = _clientStorage.GetClients();

            while (clients.Values.Any(c => c.Keys.Any(a => a.Substring(6) == $"{numcode}{randomNumber}")))
            {
                randomNumber = random.Next(0, 10000000);
            }

            return randomNumber.ToString("D7");
        }

        public Account CreateAccountForExistingClient(string numcode)
        {
            Currency currency = new Currency { Name = "US Dollar", NumCode = numcode, Symbol = "$" };
            var account = new Account
            {
                Cur = currency,
                Amount = 0,
                AccountNumber = $"2224{currency.NumCode}{KUBBank}{GenerateUniqueRandomAcc(currency.NumCode)}"
            };

            return account;
        }

        public void UpdateAccount(Client client, Account account, int newAmount)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "Клиент не может быть null.");
            }

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Лицевой счет не может быть null.");
            }

            if(newAmount < 0)
            {
                throw new AccountDataExceptions(AccountDataExceptions.AccountBalanceLessThanZeroMessage);
            }

            if (_clientStorage.GetClients().ContainsKey(client))
            {
                _clientStorage.UpdateAccount(client, account, newAmount);
            }
            else
            {
                throw new ClientDataExceptions(ClientDataExceptions.NoClientMessage);
            }
        }

        public Dictionary<Client, Dictionary<string, Account>> GetClients(string fio = "", string phone = "", string passport = "", DateOnly? dateFrom = null, DateOnly? dateTo = null)
        {
            var clients = _clientStorage.GetClients();

            if (string.IsNullOrEmpty(fio) && string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(passport) && dateFrom == null && dateTo == null)
            {
                return clients;
            }

            Expression<Func<Client, bool>> filter = c =>
                (string.IsNullOrEmpty(fio) || c.GetFullName().Contains(fio)) &&
                (string.IsNullOrEmpty(phone) || c.Telephone.Contains(phone)) &&
                (string.IsNullOrEmpty(passport) || c.PassportNumber.Contains(passport)) &&
                (!dateFrom.HasValue || c.BDate >= dateFrom) &&
                (!dateTo.HasValue || c.BDate <= dateTo);

            return clients.Where(c => filter.Compile()(c.Key)).ToDictionary(c => c.Key, c => c.Value);
        }
    }
}
