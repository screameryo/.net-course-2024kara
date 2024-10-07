using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private readonly ClientStorage _clientStorage;

        public ClientService(ClientStorage clientStorage)
        {
            _clientStorage = clientStorage ?? throw new ArgumentNullException(nameof(clientStorage), "Хранилище клиентов не может быть null.");
        }

        public void AddClient(Client newClient)
        {
            Account newAccount = CreateAccount();

            if (ValidateClient(newClient))
            {
                _clientStorage.AddClient(newClient, new List<Account> { { newAccount } });
            }
        }

        public void AddAdditionalAccount(Client client, Account account)
        {
            if (ValidateClient(client))
            {
                if (client is null)
                {
                    throw new ArgumentNullException(nameof(client), "Клиент не может быть null.");
                }

                if (account is null)
                {
                    throw new ArgumentNullException(nameof(account), "Лицевой счет не может быть null.");
                }

                _clientStorage.AddAccountToClient(client, account);
            }
        }

        private bool ValidateClient(Client client)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(client, new ValidationContext(client), validationResults, true))
            {
                throw new ClientDataException($"Неверные данные клиента {validationResults.Select(r => r.ErrorMessage).Aggregate((a, b) => $"{a}{Environment.NewLine}{b}")}");
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

            if(client.PassportNumber is null || client.PassportSeries is null)
            {
                throw new ClientDataException("У клиента отсутствуют паспортные данные.");
            }

            if (string.IsNullOrEmpty(client.PassportNumber.Trim()) || string.IsNullOrEmpty(client.PassportSeries.Trim()))
            {
                throw new ClientDataException("У клиента отсутствуют паспортные данные.");
            }

            return true;
        }

        private Account CreateAccount()
        {
            Random random = new Random();
            var account = new Account
            {
                Cur = new Currency { Name = "US Dollar", NumCode = "840", Symbol = "$" },
                Amount = 0,
                AccountNumber = $"222484066{random.Next(0, 10000000).ToString("D7")}"
            };

            return account;
        }

        public void UpdateAccount(Client client, Account account, int newAmount)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client), "Клиент не может быть null.");
            }

            if (account is null)
            {
                throw new ArgumentNullException(nameof(account), "Лицевой счет не может быть null.");
            }

            if(newAmount < 0)
            {
                throw new AccountDataException("Сумма на лицевом счете меньше 0.");
            }

            if (_clientStorage.ContainsClient(client))
            {
                _clientStorage.UpdateAccount(client, account, newAmount);
            }
            else
            {
                throw new ClientDataException("Клиент не найден.");
            }
        }

        public Dictionary<Client, List<Account>> SearchClient(string fio = "", string phone = "", string passport = "", DateOnly? dateFrom = null, DateOnly? dateTo = null)
        {
            return _clientStorage.SearchClient(fio, phone, passport, dateFrom, dateTo);
        }
    }
}
