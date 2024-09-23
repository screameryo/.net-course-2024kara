namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public Employee(string fName, string lName, DateOnly bDate, string passport, string telephone, string address, string? mName = null)
            : base(fName, lName, bDate, passport, telephone, address, mName) { }

        public string Position { get; set; }

        public int Salary { get; set; }

        public string Department { get; set; }

        public string Contract { get; set; }
    }
}
