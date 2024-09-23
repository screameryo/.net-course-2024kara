namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public Client(string FName, string LName, DateOnly BDate, string Passport, string Address, string Telephone, string? MName = null) 
            : base(FName, LName, BDate, Passport, Address, Telephone, MName) { }

        public string Ch_Account { get; set; }
    }
}
