namespace BankSystem.Domain.Models
{
    public class Person
    {
        public string FName { get; set; }

        public string LName { get; set; }

        public string? MName { get; set; }

        public DateOnly BDate { get; set; }

        public string Passport { get; set; }

        public string Telephone { get; set; }

        public string Address { get; set; }
    }
}
