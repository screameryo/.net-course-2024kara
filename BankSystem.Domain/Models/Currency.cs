namespace BankSystem.Domain.Models
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
