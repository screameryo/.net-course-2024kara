namespace BankSystem.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string NameCur { get; set; }
        public int Amount { get; set; }
        public string AccountNumber { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Account a = (Account)obj;
            return NameCur == a.NameCur && Amount == a.Amount && AccountNumber == a.AccountNumber;
        }

        public override int GetHashCode()
        {
            return NameCur.GetHashCode() + Amount.GetHashCode() + AccountNumber.GetHashCode();
        }
    }
}
