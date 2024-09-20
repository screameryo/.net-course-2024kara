namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public Client(string FName, string LName, DateOnly BDate, string Passport, string Address, string Telephone, string? MName = null) : base(FName, LName, BDate, Passport, Address, Telephone, MName) { }

        /// <summary>
        /// Get the full name of the client
        /// </summary>
        /// <returns>The full name of the client</returns>
        public string GetFullName()
        {
            return base.GetFullName();
        }

        /// <summary>
        /// Get the age of the client
        /// </summary>
        /// <returns>The age of the client</returns>
        public int GetAge()
        {
            return base.GetAge();
        }
    }
}
