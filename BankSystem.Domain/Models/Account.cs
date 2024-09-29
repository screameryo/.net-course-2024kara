namespace BankSystem.Domain.Models
{
    public class Account
    {
        public string Currency { get; set; }
        public int Amount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Account a = (Account)obj;
            return Currency == a.Currency && Amount == a.Amount;
        }
    }
}
