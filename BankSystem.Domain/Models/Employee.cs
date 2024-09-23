namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public string Position { get; set; }

        public int Salary { get; set; }

        public string Department { get; set; }

        public string Contract { get; set; }
    }
}
