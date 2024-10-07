namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Client c = (Client)obj;
            return FName == c.FName 
                && LName == c.LName 
                && BDate == c.BDate 
                && PassportNumber == c.PassportNumber 
                && PassportSeries == c.PassportSeries
                && Telephone == c.Telephone 
                && Address == c.Address;
        }

        public override int GetHashCode()
        {
            return FName.GetHashCode() 
                + LName.GetHashCode() 
                + BDate.GetHashCode() 
                + PassportNumber.GetHashCode() 
                + PassportSeries.GetHashCode() 
                + Telephone.GetHashCode() 
                + Address.GetHashCode();
        }
    }
}
