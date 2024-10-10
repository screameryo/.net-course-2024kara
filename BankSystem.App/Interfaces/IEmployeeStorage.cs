using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IEmployeeStorage : IStorage<Employee>
    {
        Dictionary<Employee, List<Account>> Data { get; }

        void AddAccount(Employee employee, Account account);
        void UpdateAccount(Employee employee, Account account);
        void DeleteAccount(Employee employee, Account account);
    }
}
