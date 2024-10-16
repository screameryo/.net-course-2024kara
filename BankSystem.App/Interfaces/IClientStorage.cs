using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IClientStorage : IStorage<Client>
    {
        void AddAccount(Guid Id);
        void UpdateAccount(Guid Id, Account account);
        void DeleteAccount(Guid Id);
        List<Account> GetAccounts(Guid Id);
    }
}
