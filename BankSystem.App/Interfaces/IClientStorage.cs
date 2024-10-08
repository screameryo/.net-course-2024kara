﻿using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IClientStorage : IStorage<Client>
    {
        Dictionary<Client, List<Account>> Data { get; }

        void AddAccount(Client client, Account account);
        void UpdateAccount(Client client, Account account);
        void DeleteAccount(Client client, Account account);
    }
}
