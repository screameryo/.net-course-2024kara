namespace BankSystem.Domain.Models
{
    public class Account
    {
        public Currency Cur { get; set; }
        public int Amount { get; set; }
        public string AccountNumber { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Account a = (Account)obj;
            return Cur.NumCode == a.Cur.NumCode && Amount == a.Amount && AccountNumber == a.AccountNumber;
        }

        public override int GetHashCode()
        {
            return Cur.GetHashCode() + Amount.GetHashCode() + AccountNumber.GetHashCode();
        }
    }
}
